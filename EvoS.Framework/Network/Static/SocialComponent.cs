using System;
using System.Collections.Generic;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(555)]
    public class SocialComponent : ICloneable
    {
        public SocialComponent()
        {
            FriendInfo = new Dictionary<long, FriendData>();
            ReportedPermanentPoints = 0;
            ReportedTemporaryPoints = 0;
            TimeOfDecay = DateTime.MinValue;
            GrantedRAFRewards = new Dictionary<int, int>();
        }

        public int ReportedPermanentPoints { get; set; }

        public int ReportedTemporaryPoints { get; set; }

        public Dictionary<int, int> GrantedRAFRewards { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public FriendData GetOrCreateFriendInfo(long friendAccountId)
        {
            FriendData friendData;
            if (!FriendInfo.TryGetValue(friendAccountId, out friendData))
            {
                friendData = new FriendData();
                FriendInfo[friendAccountId] = friendData;
            }

            return friendData;
        }

        public void UpdateLastSeenVisuals(long friendAccountId, int titleId, int titleLevel, int bgId, int fgId,
            int ribbonId, string note)
        {
            FriendData orCreateFriendInfo = GetOrCreateFriendInfo(friendAccountId);
            orCreateFriendInfo.LastSeenTitleID = titleId;
            orCreateFriendInfo.LastSeenTitleLevel = titleLevel;
            orCreateFriendInfo.LastSeenForegroundID = fgId;
            orCreateFriendInfo.LastSeenBackbroundID = bgId;
            orCreateFriendInfo.LastSeenRibbonID = ribbonId;
            orCreateFriendInfo.LastSeenNote = note;
        }

        public void UpdateNote(long friendAccountId, string note)
        {
            FriendData orCreateFriendInfo = GetOrCreateFriendInfo(friendAccountId);
            orCreateFriendInfo.LastSeenNote = note.Substring(0, Math.Min(note.Length, 50));
        }

        public void AddReportAgainst()
        {
            ReportedPermanentPoints++;
            ReportedTemporaryPoints++;
        }

        public void CalculatePointDecay(float ReportDecayValue)
        {
            if (ReportedTemporaryPoints > 0 && TimeOfDecay != DateTime.MinValue && DateTime.UtcNow > TimeOfDecay)
            {
                int num = (int) ((DateTime.UtcNow - TimeOfDecay).TotalHours * ReportDecayValue);
                if (num > ReportedTemporaryPoints)
                {
                    ReportedTemporaryPoints = 0;
                }
                else
                {
                    ReportedTemporaryPoints -= num;
                }
            }
        }

        public void CalculateNewTimeOfDecay(int muteDuration)
        {
            DateTime dateTime;
            if (muteDuration > 0)
            {
                dateTime = DateTime.UtcNow + TimeSpan.FromSeconds(muteDuration) + TimeSpan.FromHours(12.0);
            }
            else
            {
                dateTime = DateTime.UtcNow + TimeSpan.FromHours(1.0);
            }

            if (dateTime > TimeOfDecay)
            {
                TimeOfDecay = dateTime;
            }
        }

        [EvosMessage(556)]
        public Dictionary<long, FriendData> FriendInfo;
        public DateTime TimeOfDecay;

        [Serializable]
        [EvosMessage(559)]
        public class FriendData
        {
            public FriendData()
            {
                LastSeenTitleID = -1;
                LastSeenTitleLevel = -1;
                LastSeenForegroundID = -1;
                LastSeenBackbroundID = -1;
                LastSeenRibbonID = -1;
                LastSeenNote = string.Empty;
            }

            public int LastSeenTitleID;
            public int LastSeenTitleLevel;
            public int LastSeenBackbroundID;
            public int LastSeenForegroundID;
            public int LastSeenRibbonID;
            public string LastSeenNote;
        }
    }
}
