using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using EvoS.Framework.Assets.Bundles;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Logging;
using EvoS.Framework.Misc;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Assets
{
    public class AssetLoader
    {
        public AssetFile MainAssetFile;
        private string _basePath;

        private Dictionary<string, WeakReference<AssetFile>> _assetFiles =
            new Dictionary<string, WeakReference<AssetFile>>();

        private Dictionary<string, UnityFs> _assetBundles =
            new Dictionary<string, UnityFs>();

        private List<AssetFile> _strongRefs = new List<AssetFile>();

        private Dictionary<byte[], List<SerializedMonoScript>> _scriptsByHash =
            new Dictionary<byte[], List<SerializedMonoScript>>(new ByteArrayComparer());

        public readonly Dictionary<string, SerializedMonoScript> ScriptsByName =
            new Dictionary<string, SerializedMonoScript>();

        public Dictionary<string, SerializedGameObject> NetObjsByName = new Dictionary<string, SerializedGameObject>();

        public Dictionary<NetworkHash128, SerializedGameObject> NetObjsByAssetId =
            new Dictionary<NetworkHash128, SerializedGameObject>();

        public Dictionary<string, SerializedGameObject>.ValueCollection NetworkedObjects => NetObjsByName.Values;

        public AssetLoader(string basePath)
        {
            _basePath = basePath;
        }

        public SerializedGameObject GetNetObj(string assetId)
        {
            return GetNetObj(new NetworkHash128(Utils.ToByteArray(assetId)));
        }

        public SerializedGameObject GetNetObj(NetworkHash128 assetId)
        {
            return NetObjsByAssetId[assetId];
        }

        public AssetFile LoadAsset(string name, bool strongRef = false)
        {
            AssetFile assetFile = null;

            if (_assetFiles.TryGetValue(name, out var existingRef) && existingRef.TryGetTarget(out assetFile))
            {
                return assetFile;
            }

            var assetIsMain = MainAssetFile == null;

            // check if the asset is part of a bundle
            if (name.StartsWith("archive:/"))
            {
                var nameInfo = name.Split("/");
                var bundleName = nameInfo[1];
                var assetName = nameInfo[2];

                // check if the bundled asset is already loaded
                if (_assetFiles.TryGetValue(assetName, out existingRef) && existingRef.TryGetTarget(out assetFile))
                {
                    return assetFile;
                }

                // ensure the bundle is loaded
                if (!_assetBundles.TryGetValue(bundleName, out var unityFs))
                {
                    throw new ArgumentException($"Asset bundle containing asset file {name} not loaded!");
                }

                foreach (var node in unityFs.Nodes)
                {
                    if (node.Name.ToLower() != assetName) continue;

                    assetFile = unityFs.OpenAssetFile(node);
                    break;
                }

                if (assetFile == null)
                {
                    throw new ArgumentException($"Asset {assetName} not found in bundle {bundleName}!");
                }
            }
            else
            {
                assetFile = new AssetFile(this, name, new StreamReader(Path.Join(_basePath, name)));
            }

            _assetFiles[assetFile.Name] = new WeakReference<AssetFile>(assetFile);
            _assetFiles[name] = new WeakReference<AssetFile>(assetFile);

            // First loaded becomes "main" -- TODO this is arbitrary
            if (assetIsMain) MainAssetFile = assetFile;

            if (strongRef) _strongRefs.Add(assetFile);

            return assetFile;
        }

        public List<AssetFile> LoadAssetBundle(string fileName, bool loadNodes = false, bool strongRefs = false)
        {
            var unityFs = new UnityFs(this, Path.Join(_basePath, fileName));

            _assetBundles.Add(unityFs.Name, unityFs);

            if (!loadNodes)
            {
                return null;
            }

            var assets = new List<AssetFile>();
            foreach (var node in unityFs.Nodes)
            {
                assets.Add(LoadAsset($"archive:/{unityFs.Name}/{node.Name.ToLower()}", strongRefs));
            }

            return assets;
        }

        public void ConstructCaches()
        {
            ConstructMonoScriptMap();

            LoadNetworkedObjects();
//            DumpNetworkObjectComponents();
        }

        public IEnumerable<SerializedGameObject> GetObjectsByComponent(SerializedMonoScript script)
        {
            foreach (var assetFile in AllAssetFiles())
            {
                foreach (var gameObject in assetFile.GetObjectsByComponent(script))
                {
                    yield return gameObject;
                }
            }
        }

        private IEnumerable<AssetFile> AllAssetFiles()
        {
            var stack = new Stack<AssetFile>();
            var seen = new HashSet<string>();
            stack.Push(MainAssetFile);
            foreach (var file in _assetFiles.Values)
            {
                if (file.TryGetTarget(out var asset))
                {
                    stack.Push(asset);
                }
            }

            while (!stack.IsNullOrEmpty())
            {
                var assetFile = stack.Pop();
                if (seen.Contains(assetFile.Name))
                {
                    continue;
                }

                yield return assetFile;
                seen.Add(assetFile.Name);

                foreach (var extRef in assetFile.ExternalAssetRefs)
                {
                    stack.Push(extRef);
                }
            }
        }

        private void ConstructMonoScriptMap()
        {
            foreach (var assetFile in AllAssetFiles())
            {
                var monoScriptType = assetFile.FindTypeById(CommonTypeIds.MonoScript);
                if (monoScriptType == null)
                {
                    continue;
                }

                foreach (var gameObject in assetFile.GetObjectInfosByType(monoScriptType))
                {
                    var obj = (SerializedMonoScript) assetFile.ReadObject(gameObject);
                    if (obj.PropertiesHash.IsZero())
                    {
                        continue;
                    }

                    // multiple scripts can share the same properties hash
                    if (!_scriptsByHash.TryGetValue(obj.PropertiesHash.ToByteArray(), out var scriptList))
                    {
                        scriptList = new List<SerializedMonoScript>();
                        _scriptsByHash.Add(obj.PropertiesHash.ToByteArray(), scriptList);
                    }

                    scriptList.Add(obj);
                    if (!ScriptsByName.TryAdd(obj.QualifiedName, obj))
                    {
                        var existing = ScriptsByName[obj.QualifiedName];
                        if (existing.QualifiedName == obj.QualifiedName && existing.AssemblyName == obj.AssemblyName)
                        {
                            continue;
                        }

                        Log.Print(LogType.Warning, $"    trying:  {obj}");
                        Log.Print(LogType.Warning, $"    already: {ScriptsByName[obj.QualifiedName]}");
                    }
                }
            }

            Log.Print(LogType.Misc, $"Loaded {ScriptsByName.Count} MonoScript type mappings");
        }

        private void LoadNetworkedObjects()
        {
            var count = 0;
            foreach (var assetFile in AllAssetFiles())
            {
                InternalLoadNetworkedObjects(assetFile);
                count++;
            }

            Log.Print(LogType.Misc,
                $"Loaded {NetworkedObjects.Count} networked game objects from {count} asset files.");
        }

        private void InternalLoadNetworkedObjects(AssetFile assetFile)
        {
            if (!ScriptsByName.TryGetValue(CommonTypes.NetworkIdentity, out var netIdentScript))
            {
                return;
            }

            foreach (var obj in assetFile.GetObjectsByComponent(netIdentScript))
            {
                var netIdent = obj.GetComponent<NetworkIdentity>();
                if (netIdent != null)
                {
                    var netHash = new NetworkHash128(netIdent.AssetId.Bytes);
                    if (!netHash.IsZero())
                    {
                        NetObjsByName.Add(obj.Name, obj);
                        NetObjsByAssetId.Add(netHash, obj);
                    }
                }
            }
        }

        public void DumpNetworkObjectComponents()
        {
            foreach (var obj in NetworkedObjects)
            {
                var netIdent = obj.GetComponent<NetworkIdentity>();

                Log.Print(LogType.Misc, $"{netIdent.AssetId.ToHex()} {obj.Name}");
                Log.Print(LogType.Misc, $"  {string.Join(", ", obj.ComponentNames())}");
            }
        }

        public void Dispose(AssetFile assetFile)
        {
            _strongRefs.Remove(assetFile);
            _assetFiles.Remove(assetFile.Name);
        }
    }
}
