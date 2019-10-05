using System;
using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    public class StandardActorEffectData
    {
        public int m_duration = 1;
        public HealingType m_healingType = HealingType.Effect;
        public string m_effectName;
        public int m_maxStackSize;
        public int m_damagePerTurn;
        public int m_healingPerTurn;
        public int m_perTurnHitDelayTurns;
        public int m_absorbAmount;
        public int m_nextTurnAbsorbAmount;
        public bool m_dontEndEarlyOnShieldDeplete;
        public int m_damagePerMoveSquare;
        public int m_healPerMoveSquare;
        public int m_techPointLossPerMoveSquare;
        public int m_techPointGainPerMoveSquare;
        public int m_techPointChangeOnStart;
        public int m_techPointGainPerTurn;
        public int m_techPointLossPerTurn;
        public StandardActorEffectData.InvisibilityBreakMode m_invisBreakMode;
        public bool m_removeInvisibilityOnLastResolveStart;
        public bool m_removeRevealedOnLastResolveStart;
        public AbilityStatMod[] m_statMods;
        public StatusType[] m_statusChanges;
        public StandardActorEffectData.StatusDelayMode m_statusDelayMode;
        public EffectEndTag[] m_endTriggers;
        public GameObject[] m_sequencePrefabs;
        public GameObject m_tickSequencePrefab;

        public void InitWithDefaultValues()
        {
            this.SetValues(string.Empty, 1, 0, 0, 0, HealingType.Effect, 0, 0, new AbilityStatMod[0], new StatusType[0],
                StandardActorEffectData.StatusDelayMode.DefaultBehavior);
        }

        public void SetValues(
            string effectName,
            int duration,
            int maxStackSize,
            int damagePerTurn,
            int healingPerTurn,
            HealingType healingType,
            int perTurnHitDelayTurns,
            int absorbAmount,
            AbilityStatMod[] statMods,
            StatusType[] statusChanges,
            StandardActorEffectData.StatusDelayMode statusDelayMode)
        {
            this.m_effectName = effectName;
            this.m_duration = duration;
            this.m_maxStackSize = maxStackSize;
            this.m_damagePerTurn = damagePerTurn;
            this.m_healingPerTurn = healingPerTurn;
            this.m_healingType = healingType;
            this.m_perTurnHitDelayTurns = perTurnHitDelayTurns;
            this.m_absorbAmount = absorbAmount;
            this.m_statMods = statMods;
            this.m_statusChanges = statusChanges;
            this.m_statusDelayMode = statusDelayMode;
            this.m_endTriggers = new EffectEndTag[0];
            this.m_sequencePrefabs = new GameObject[0];
        }

        public enum InvisibilityBreakMode
        {
            RemoveInvisAndEndEarly,
            SuppressOnly,
        }

        public enum StatusDelayMode
        {
            DefaultBehavior,
            AllStatusesDelayToTurnStart,
            NoStatusesDelayToTurnStart,
        }
    }
}
