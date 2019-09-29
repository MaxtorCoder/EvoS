using System;
using System.Collections.Generic;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(240)]
    public class MatchResultsNotification : WebSocketMessage
    {
        [EvosMessage(244)]
        public List<CurrencyReward> CurrencyRewards;
        public int BaseXpGained;
        public int WinXpGained;
        public int GGXpGained;
        public int ConsumableXpGained;
        public int PlayWithFriendXpGained;
        public int QuestXpGained;
        public int EventBonusXpGained;
        public int FirstWinXpGained;
        public int QueueTimeXpGained;
        public int FreelancerOwnedXPGained;
        public int AccountLevelAtStart = 1;
        public int CharacterLevelAtStart = 1;
        public int AccountXpAtStart;
        public int CharacterXpAtStart;
        public int SeasonLevelAtStart;
        public int SeasonXpAtStart;
        public int FactionCompetitionId;
        public int FactionId;
        public Dictionary<string, int> FactionContributionAmounts;
        [EvosMessage(247)]
        public List<BadgeAndParticipantInfo> BadgeAndParticipantsInfo;
        public bool FirstWinOccured;
        public float TotalBadgePoints;
        public int NumCharactersPlayed;

        [Serializable]
        [EvosMessage(246)]
        public class CurrencyReward
        {
            public CurrencyType Type;
            public int BaseGained;
            public int EventGained;
            public int WinGained;
            public int QuestGained;
            public int GGGained;
            public int LevelUpGained;
        }
    }
}
