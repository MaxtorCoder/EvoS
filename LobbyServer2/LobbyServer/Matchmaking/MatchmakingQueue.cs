using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Static;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentralServer.LobbyServer.Matchmaking
{
    class MatchmakingQueue
    {
        Dictionary<string, LobbyGameInfo> Games = new Dictionary<string, LobbyGameInfo>();
        List<LobbyPlayerInfo> QueuedPlayers = new List<LobbyPlayerInfo>();
        GameType GameType;
        
        private static int GameID = 0;

        public MatchmakingQueue(GameType gameType)
        {
            GameType = gameType;
        }
        public void Update()
        {
            
        }

        public LobbyGameInfo CreateGameInfo()
        {
            GameTypeAvailability gameAvailability = Gamemode.GameModeManager.GetGameTypeAvailabilities()[GameType];

            LobbyGameInfo gameInfo = new LobbyGameInfo
            {
                AcceptedPlayers = gameAvailability.TeamAPlayers + gameAvailability.TeamBPlayers,
                AcceptTimeout = GameType == GameType.Practice ? TimeSpan.FromSeconds(0) : TimeSpan.FromSeconds(5),
                ActiveHumanPlayers = 0,
                ActivePlayers = 0,
                ActiveSpectators = 0,
                CreateTimestamp = DateTime.Now.Ticks,
                GameConfig = new LobbyGameConfig
                {
                    GameOptionFlags = GameOptionFlag.AllowDuplicateCharacters & GameOptionFlag.EnableTeamAIOutput & GameOptionFlag.NoInputIdleDisconnect,
                    GameType = GameType,
                    IsActive = true,
                    RoomName = $"Evos-{GameType.ToString()}-{GameID++}",
                    SubTypes = gameAvailability.SubTypes,
                    TeamAPlayers = gameAvailability.TeamAPlayers,
                    TeamABots = gameAvailability.TeamABots,
                    TeamBPlayers = gameAvailability.TeamBPlayers,
                    TeamBBots = gameAvailability.TeamBBots,
                    Map = SelectMap(gameAvailability),
                    ResolveTimeoutLimit = 1600,
                    Spectators = 0
                },
                GameResult = GameResult.NoResult,
                GameServerAddress = "",
                GameStatus = GameStatus.Assembling,
                GameServerHost = "",
                IsActive = true,
                LoadoutSelectTimeout = GameType==GameType.Practice ? TimeSpan.FromSeconds(0) : TimeSpan.FromSeconds(5),
                SelectTimeout = GameType==GameType.Practice ? TimeSpan.FromSeconds(0) : TimeSpan.FromSeconds(5),
                // TODO: there are more options that may be usefull
            };

            return gameInfo;
        }

        public string SelectMap(GameTypeAvailability gameTypeAvailability)
        {
            List<GameMapConfig> maps = gameTypeAvailability.SubTypes[0].GameMapConfigs;
            Random rand = new Random();
            int index = rand.Next(0, maps.Count);
            return maps[index].Map;
        }

        public void SetGameStatus(string roomName, GameStatus gameStatus)
        {
            Games[roomName].GameStatus = gameStatus;
        }

    }
}
