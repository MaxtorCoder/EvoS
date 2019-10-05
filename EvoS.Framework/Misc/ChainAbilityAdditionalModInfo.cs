using System;

namespace EvoS.Framework.Misc
{
    [Serializable]
    public class ChainAbilityAdditionalModInfo
    {
//        [Header("-- 0 based index of chain ability as it appears in main ability's list")]
        public int m_chainAbilityIndex;

//        [Header("-- Effects")] public StandardEffectInfo m_effectOnSelf;
        public StandardEffectInfo m_effectOnAlly;
        public StandardEffectInfo m_effectOnEnemy;
//        [Header("-- For Cooldown Reductions")] public AbilityModCooldownReduction m_cooldownReductionsOnSelf;

//        [Header("-- Sequence for Timing (for self hit if not already hitting)")]
//        public GameObject m_timingSequencePrefab;
    }
}
