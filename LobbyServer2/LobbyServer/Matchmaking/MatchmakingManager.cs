using CentralServer.BridgeServer;
using CentralServer.LobbyServer.Character;
using CentralServer.LobbyServer.Gamemode;
using CentralServer.LobbyServer.Session;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.Static;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentralServer.LobbyServer.Matchmaking
{
    public static class MatchmakingManager
    {
        public static void StartPractice(LobbyServerProtocolBase client)
        {
            LobbyGameInfo practiceGameInfo = new LobbyGameInfo
            {
                AcceptedPlayers = 1,
                AcceptTimeout = new TimeSpan(0,0,0),
                ActiveHumanPlayers = 1,
                ActivePlayers = 1,
                CreateTimestamp = DateTime.Now.Ticks,
                GameConfig = new LobbyGameConfig 
                {
                    GameOptionFlags = GameOptionFlag.NoInputIdleDisconnect & GameOptionFlag.NoInputIdleDisconnect,
                    GameServerShutdownTime = -1,
                    GameType = GameType.Practice,
                    InstanceSubTypeBit = 1,
                    IsActive = true,
                    Map = Maps.VR_Practice,
                    ResolveTimeoutLimit = 1600, // TODO ?
                    RoomName = "",
                    Spectators = 0,
                    SubTypes = GameModeManager.GetGameTypeAvailabilities()[GameType.Practice].SubTypes,
                    TeamABots = 0,
                    TeamAPlayers = 1,
                    TeamBBots = 2,
                    TeamBPlayers = 0
                }
            };

            LobbyTeamInfo teamInfo = new LobbyTeamInfo();
            teamInfo.TeamPlayerInfo = new List<LobbyPlayerInfo>
            {
                SessionManager.GetPlayerInfo(client.AccountId),
                CharacterManager.GetPunchingDummyPlayerInfo(),
                CharacterManager.GetPunchingDummyPlayerInfo()
            };
            string serverAddress = ServerManager.GetServer(practiceGameInfo, teamInfo);
            if (serverAddress == null)
            {
                Log.Print(LogType.Error, "No available server for practice gamemode");
            }
        }
    }
}
