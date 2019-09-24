using System;
using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(593)]
    public class AdminComponent
    {
        public AdminComponent()
        {
            AdminActions = new List<AdminActionRecord>();
            ActiveQueuePenalties = new Dictionary<GameType, QueuePenalties>();
            Region = Region.US;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public AdminComponent CloneForClient()
        {
            return new AdminComponent
            {
                GameLeavingPoints = GameLeavingPoints
            };
        }

        public bool Locked { get; set; }

        public DateTime LockedUntil { get; set; }

        public bool Muted { get; set; }

        public DateTime MutedUntil { get; set; }

        public bool Online { get; set; }

        public DateTime LastLogin { get; set; }

        public DateTime LastLogout { get; set; }

        public string LastLogoutSessionToken { get; set; }

        public Region Region { get; set; }

        public string LanguageCode { get; set; }

        [EvosMessage(594)]
        public List<AdminActionRecord> AdminActions { get; set; }

        [EvosMessage(598)]
        public Dictionary<GameType, QueuePenalties> ActiveQueuePenalties { get; set; }

        public float GameLeavingPoints { get; set; }

        public DateTime GameLeavingLastForgivenessCheckpoint { get; set; }

        [EvosMessage(597)]
        public enum AdminActionType
        {
            Lock,
            Unlock,
            Mute,
            Unmute,
            Kick,
            Alter
        }

        [Serializable]
        [EvosMessage(596)]
        public class AdminActionRecord
        {
            public string AdminUsername { get; set; }

            public AdminActionType ActionType { get; set; }

            public TimeSpan Duration { get; set; }

            public string Description { get; set; }

            public DateTime Time { get; set; }
        }
    }
}
