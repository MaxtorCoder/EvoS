using System;
using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Misc;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(476)]
    public class LobbyGameInfo
    {
        public LobbyGameInfo()
        {
            ggPackUsedAccountIDs = new Dictionary<long, int>();
        }

        public LobbyGameInfo Clone()
        {
            return (LobbyGameInfo) MemberwiseClone();
        }

        [JsonIgnore]
        public string Name
        {
            get
            {
                if (GameServerProcessCode.IsNullOrEmpty() || GameConfig == null)
                {
                    return "unknown";
                }

//                if (!GameConfig.HasSelectedSubType)
//                {
                return $"{GameServerProcessCode} ({GameServerAddress}) [{GameConfig.Map} {GameConfig.GameType}]";
//                }

//                return $"{GameServerProcessCode} ({GameServerAddress}) [{GameConfig.Map} {GameConfig.GameType} {GameConfig.InstanceSubType.GetNameAsPayload().Term}]";
            }
        }

        public string MonitorServerProcessCode;
        public string GameServerProcessCode;
        public string GameServerAddress;
        public string GameServerHost;
        public long CreateTimestamp;
        public long UpdateTimestamp;
        public long SelectionStartTimestamp;
        public long SelectionSubPhaseStartTimestamp;
        public long LoadoutSelectionStartTimestamp;
        public GameStatus GameStatus = GameStatus.Stopped;
        public GameResult GameResult;
        public FreelancerResolutionPhaseSubType SelectionSubPhase =
            FreelancerResolutionPhaseSubType.WAITING_FOR_ALL_PLAYERS;
        public TimeSpan AcceptTimeout;
        public TimeSpan SelectTimeout;
        public TimeSpan LoadoutSelectTimeout;
        public TimeSpan SelectSubPhaseBan1Timeout;
        public TimeSpan SelectSubPhaseBan2Timeout;
        public TimeSpan SelectSubPhaseFreelancerSelectTimeout;
        public TimeSpan SelectSubPhaseTradeTimeout;
        public bool IsActive;
        public int ActivePlayers;
        public int ActiveHumanPlayers;
        public int ActiveSpectators;
        public int AcceptedPlayers;
        public BotDifficulty SelectedBotSkillTeamA;
        public BotDifficulty SelectedBotSkillTeamB;
        [EvosMessage(477)] public Dictionary<long, int> ggPackUsedAccountIDs;
        [EvosMessage(483)] public Dictionary<long, Dictionary<int, int>> AccountIdToOverconIdToCount;
        public LobbyGameConfig GameConfig;
    }
}
