using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("BotController")]
    public class BotController : MonoBehaviour
    {
        public float m_combatRange;
        public float m_idealRange;
        public float m_retreatFromRange;

        public BotController()
        {
        }

        public BotController(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_combatRange = stream.ReadSingle();
            m_idealRange = stream.ReadSingle();
            m_retreatFromRange = stream.ReadSingle();
        }

        public override string ToString()
        {
            return $"{nameof(BotController)}(" +
                   $"{nameof(m_combatRange)}: {m_combatRange}, " +
                   $"{nameof(m_idealRange)}: {m_idealRange}, " +
                   $"{nameof(m_retreatFromRange)}: {m_retreatFromRange}, " +
                   ")";
        }
    }
}
