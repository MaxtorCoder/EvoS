using System;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    public abstract class AlertMissionBase
    {
        protected void CopyBaseVariables(AlertMissionBase other)
        {
            Type = other.Type;
            DurationHours = other.DurationHours;
            QuestId = other.QuestId;
            BonusType = other.BonusType;
            BonusMultiplier = other.BonusMultiplier;
        }

        public AlertMissionType Type;
        public float DurationHours;
        public int QuestId;
        public CurrencyType BonusType;
        public int BonusMultiplier;
    }
}
