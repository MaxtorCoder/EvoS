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
        public Dictionary<string, SerializedGameObject> NetObjsByName = new Dictionary<string, SerializedGameObject>();

        public Dictionary<NetworkHash128, SerializedGameObject> NetObjsByAssetId =
            new Dictionary<NetworkHash128, SerializedGameObject>();

        private TypeEntry _gameObjectType;

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

            LoadNetworkedObjects();
        }

        private void LoadNetworkedObjects()
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

                InternalLoadNetworkedObjects(assetFile);
                seen.Add(assetFile.Name);

                foreach (var extRef in assetFile.ExternalAssetRefs)
                {
                    stack.Push(extRef);
                }
            }

            Log.Print(LogType.Misc, $"Loaded {NetworkedObjects.Count} networked game objects");
        }

        private void InternalLoadNetworkedObjects(AssetFile assetFile)
        {
            Console.WriteLine($"Loading net objs from {assetFile.Name}");

            var gameObjectType = assetFile.FindTypeById(1);
            if (gameObjectType == null)
            {
                return;
            }

            foreach (var gameObject in assetFile.GetObjectInfosByType(gameObjectType))
            {
                var obj = (SerializedGameObject) assetFile.ReadObject(gameObject);

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
