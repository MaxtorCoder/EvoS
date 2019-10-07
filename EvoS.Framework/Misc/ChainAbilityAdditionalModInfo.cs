using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;

namespace EvoS.Framework.Misc
{
    [Serializable]
    public class ChainAbilityAdditionalModInfo : ISerializedItem
    {
//        [Header("-- 0 based index of chain ability as it appears in main ability's list")]
        public int m_chainAbilityIndex;

//        [Header("-- Effects")]
        public StandardEffectInfo m_effectOnSelf;
        public StandardEffectInfo m_effectOnAlly;
        public StandardEffectInfo m_effectOnEnemy;

//        [Header("-- For Cooldown Reductions")]
        public AbilityModCooldownReduction m_cooldownReductionsOnSelf;

//        [Header("-- Sequence for Timing (for self hit if not already hitting)")]
        public SerializedComponent m_timingSequencePrefab;

        public ChainAbilityAdditionalModInfo()
        {
        }

        public ChainAbilityAdditionalModInfo(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_chainAbilityIndex = stream.ReadInt32();
            m_effectOnSelf = new StandardEffectInfo(assetFile, stream);
            m_effectOnAlly = new StandardEffectInfo(assetFile, stream);
            m_effectOnEnemy = new StandardEffectInfo(assetFile, stream);
            m_cooldownReductionsOnSelf = new AbilityModCooldownReduction(assetFile, stream);
            m_timingSequencePrefab = new SerializedComponent(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(ChainAbilityAdditionalModInfo)}>(" +
                   $"{nameof(m_chainAbilityIndex)}: {m_chainAbilityIndex}, " +
                   $"{nameof(m_effectOnSelf)}: {m_effectOnSelf}, " +
                   $"{nameof(m_effectOnAlly)}: {m_effectOnAlly}, " +
                   $"{nameof(m_effectOnEnemy)}: {m_effectOnEnemy}, " +
                   $"{nameof(m_cooldownReductionsOnSelf)}: {m_cooldownReductionsOnSelf}, " +
                   $"{nameof(m_timingSequencePrefab)}: {m_timingSequencePrefab}, " +
                   ")";
        }
    }
}
