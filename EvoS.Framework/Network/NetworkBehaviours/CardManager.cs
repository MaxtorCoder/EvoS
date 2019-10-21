using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("CardManager")]
    public class CardManager : NetworkBehaviour
    {
        public SerializedComponent m_dataPrefab;

        public CardManager()
        {
        }

        public CardManager(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override bool OnSerialize(NetworkWriter writer, bool forceAll)
        {
            return false;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_dataPrefab = new SerializedComponent(assetFile, stream); // class [UnityEngine]UnityEngine.GameObject
        }

        public override string ToString()
        {
            return $"{nameof(CardManager)}(" +
                   $"{nameof(m_dataPrefab)}: {m_dataPrefab}, " +
                   ")";
        }
    }
}
