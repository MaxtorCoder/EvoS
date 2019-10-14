using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("InterfaceManager")]
    public class InterfaceManager : NetworkBehaviour
    {
        public SerializedComponent m_backgroundTexture;
        public float m_combatTextLifetime;
        public float m_combatTextScrollSpeed;
        public int m_combatTextSize;

        public InterfaceManager()
        {
        }

        public InterfaceManager(AssetFile assetFile, StreamReader stream)
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
            m_backgroundTexture =
                new SerializedComponent(assetFile, stream);
            m_combatTextLifetime = stream.ReadSingle();
            m_combatTextScrollSpeed = stream.ReadSingle();
            m_combatTextSize = stream.ReadInt32();
        }

        public override string ToString()
        {
            return $"{nameof(InterfaceManager)}(" +
                   $"{nameof(m_backgroundTexture)}: {m_backgroundTexture}, " +
                   $"{nameof(m_combatTextLifetime)}: {m_combatTextLifetime}, " +
                   $"{nameof(m_combatTextScrollSpeed)}: {m_combatTextScrollSpeed}, " +
                   $"{nameof(m_combatTextSize)}: {m_combatTextSize}, " +
                   ")";
        }
    }
}
