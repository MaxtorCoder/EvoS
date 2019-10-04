using System;
using System.Collections.Generic;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Assets
{
    public class AssetLoader
    {
        public AssetFile MainAssetFile;
        public Dictionary<string, SerializedGameObject> NetObjsByName = new Dictionary<string, SerializedGameObject>();

        public Dictionary<NetworkHash128, SerializedGameObject> NetObjsByAssetId =
            new Dictionary<NetworkHash128, SerializedGameObject>();

        public Dictionary<string, SerializedGameObject>.ValueCollection NetworkedObjects => NetObjsByName.Values;

        public void Load(string join)
        {
            NetObjsByName.Clear();
            NetObjsByAssetId.Clear();
            MainAssetFile = new AssetFile(join);

            var gameObjectType = MainAssetFile.FindTypeById(1);
            foreach (var gameObject in MainAssetFile.GetObjectInfosByType(gameObjectType))
            {
                var obj = (SerializedGameObject) MainAssetFile.ReadObject(gameObject);

                var netIdent = obj.GetComponent<SerializedNetworkIdentity>();
                if (netIdent != null)
                {
                    NetObjsByName.Add(obj.Name, obj);
                    NetObjsByAssetId.Add(new NetworkHash128(netIdent.AssetId.Bytes), obj);
                }
            }

            Log.Print(LogType.Misc, $"Loaded {NetworkedObjects.Count} networked game objects");
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
