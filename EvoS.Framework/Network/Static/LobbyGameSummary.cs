using System;
using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    public class LobbyGameSummary
    {
        public string GameServerAddress;
        public GameResult GameResult;
        public float GameResultFraction = 0.5f;
        public string TimeText = string.Empty;
        public int NumOfTurns;
        public int TeamAPoints;
        public int TeamBPoints;
        public TimeSpan MatchTime;
        public List<PlayerGameSummary> PlayerGameSummaryList = new List<PlayerGameSummary>();
//        public Dictionary<Team, Dictionary<int, ELODancecard>> m_ELODancecard =
//            new Dictionary<Team, Dictionary<int, ELODancecard>>();
        public List<BadgeAndParticipantInfo> BadgeAndParticipantsInfo;
    }
}
