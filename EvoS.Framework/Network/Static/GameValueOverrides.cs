using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(161)]
    public class GameValueOverrides
    {
        public void SetIntOverride(OverrideAbleGameValue overrideValueType, int? value)
        {
            if (overrideValueType == OverrideAbleGameValue.InitialTimeBankConsumables)
            {
                InitialTimeBankConsumables = value;
            }
        }

        public void SetTimeSpanOverride(OverrideAbleGameValue overrideValueType, TimeSpan? value)
        {
            if (overrideValueType == OverrideAbleGameValue.TurnTimeSpan)
            {
                TurnTimeSpan = value;
            }
        }

        public int? InitialTimeBankConsumables;

        public TimeSpan? TurnTimeSpan;

        public enum OverrideAbleGameValue
        {
            None,
            InitialTimeBankConsumables,
            TurnTimeSpan
        }
    }
}
