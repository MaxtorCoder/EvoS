using System;
using System.Collections.Generic;
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

        private Dictionary<byte[], List<SerializedMonoScript>> _scriptsByHash =
            new Dictionary<byte[], List<SerializedMonoScript>>(new ByteArrayComparer());

        private Dictionary<string, SerializedMonoScript> _scriptsByName =
            new Dictionary<string, SerializedMonoScript>();

        public Dictionary<string, SerializedGameObject> NetObjsByName = new Dictionary<string, SerializedGameObject>();

        public Dictionary<NetworkHash128, SerializedGameObject> NetObjsByAssetId =
            new Dictionary<NetworkHash128, SerializedGameObject>();

        public Dictionary<string, SerializedGameObject>.ValueCollection NetworkedObjects => NetObjsByName.Values;

        public SerializedGameObject GetNetObj(string assetId)
        {
            return GetNetObj(new NetworkHash128(Utils.ToByteArray(assetId)));
        }

        public SerializedGameObject GetNetObj(NetworkHash128 assetId)
        {
            return NetObjsByAssetId[assetId];
        }

        public void Load(string join)
        {
            NetObjsByName.Clear();
            NetObjsByAssetId.Clear();
            MainAssetFile = new AssetFile(join);

            _gameObjectType = MainAssetFile.FindTypeById(1);
            ConstructMonoScriptMap();

            LoadNetworkedObjects();

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
                    if (!_scriptsByName.TryAdd(obj.QualifiedName, obj))
                    {
                        var existing = _scriptsByName[obj.QualifiedName];
                        if (existing.QualifiedName == obj.QualifiedName && existing.AssemblyName == obj.AssemblyName)
                        {
                            continue;
                        }

                        Log.Print(LogType.Warning, $"    trying:  {obj}");
                        Log.Print(LogType.Warning, $"    already: {_scriptsByName[obj.QualifiedName]}");
                    }
                }
            }

            Log.Print(LogType.Misc, $"Loaded {_scriptsByName.Count} MonoScript type mappings");
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
            var netIdentScript = _scriptsByName[CommonTypes.NetworkIdentity];
            foreach (var obj in assetFile.GetObjectsByComponent(netIdentScript))
            {
                var netIdent = obj.GetComponent<SerializedNetworkIdentity>();
                if (netIdent != null)
                {
                    NetObjsByName.Add(obj.Name, obj);
                    NetObjsByAssetId.Add(new NetworkHash128(netIdent.AssetId.Bytes), obj);
                }
            }
        }

        public void DumpNetworkObjectComponents()
        {
            foreach (var obj in NetworkedObjects)
            {
                var netIdent = obj.GetComponent<SerializedNetworkIdentity>();

                Log.Print(LogType.Misc, $"{netIdent.AssetId.ToHex()} {obj.Name}");
                Log.Print(LogType.Misc, $"  {string.Join(", ", obj.ComponentNames())}");
            }
        }

        public static AssetLoader Instance { get; } = new AssetLoader();
    }
}
