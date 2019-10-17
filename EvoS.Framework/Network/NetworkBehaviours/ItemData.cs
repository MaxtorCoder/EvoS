using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Game;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("ItemData")]
    public class ItemData : NetworkBehaviour
    {
        private int _credits;
        private int _creditsSpent;

        public int Credits => _credits;
        public int CreditsSpent => _creditsSpent;

        public ItemData()
        {
        }

        public ItemData(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override bool OnSerialize(NetworkWriter writer, bool initialState)
        {
            if (!initialState)
                writer.WritePackedUInt32(syncVarDirtyBits);
            if (!initialState && syncVarDirtyBits == 0U)
                return false;
            OnSerializeHelper(new NetworkWriterAdapter(writer));
            return true;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            var num = uint.MaxValue;
            if (!initialState)
                num = reader.ReadPackedUInt32();
            if (num == 0U)
                return;
            OnSerializeHelper(new NetworkReaderAdapter(reader));
        }

        private void OnSerializeHelper(IBitStream stream)
        {
//            if (EvoSGameConfig.NetworkIsServer && stream.isReading)
//                return;
            var num1 = 0;
            var num2 = 0;
            if (stream.isWriting)
            {
                var credits = _credits;
                var creditsSpent = _creditsSpent;
                stream.Serialize(ref credits);
                stream.Serialize(ref creditsSpent);
            }
            else
            {
                stream.Serialize(ref num1);
                stream.Serialize(ref num2);
                _credits = num1;
                _creditsSpent = num2;
            }
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
        }

        public override string ToString()
        {
            return $"{nameof(ItemData)}>(" +
                   ")";
        }
    }
}
