using System.Collections.Generic;
using EvoS.Framework.Network.Game.Messages;
using EvoS.Framework.Network.NetworkMessages;
using EvoS.Framework.Network.Static;

namespace EvoS.Framework.Game
{
    public class GameManagerHolder
    {
        private static Dictionary<string, GameManager> _gameManagers = new Dictionary<string, GameManager>();
        private static Dictionary<long, GameManager> _gameManagersByPlayerAccountID = new Dictionary<long, GameManager>();

        class EmptyRoomNameGameServerException : System.Exception { }
        class InvalidPlayerAccountIDGameServerException : System.Exception { }

        /// <summary>
        /// Creates a GameManager instance which holds each match. Called from the lobby queue
        /// </summary>
        /// <param name="gameInfo">Match configuration</param>
        /// <param name="teamInfo">players information (players + bots)</param>
        public static void CreateGameManager(LobbyGameInfo gameInfo, LobbyTeamInfo teamInfo)
        {
            if (string.IsNullOrEmpty(gameInfo.GameConfig.RoomName)) {
                throw new EmptyRoomNameGameServerException();
            }
            try
            {
                // Create a GameManager for this new game
                GameManager gameManager = new GameManager();
                gameManager.SetGameInfo(gameInfo);
                gameManager.SetTeamInfo(teamInfo);

                // Store the players SessionToken to identify them when they connect to this server
                foreach (LobbyPlayerInfo player in teamInfo.TeamPlayerInfo)
                {
                    if (!player.IsNPCBot)
                    {
                        if (player.AccountId == 0) // Each player must have a unique AccountId
                            throw new InvalidPlayerAccountIDGameServerException();

                        _gameManagersByPlayerAccountID.Add(player.AccountId, gameManager);
                    }
                }

                _gameManagers.Add(gameInfo.GameConfig.RoomName, gameManager);
                gameManager.LaunchGame();
            }
            catch (System.Exception e)
            {
                throw new EvosException("Error Creating Game Manager", e);
            }
        }

        public static GameManager? FindGameManager(long playerAccountId)
        {
            try
            {
                return _gameManagersByPlayerAccountID[playerAccountId];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
            /*
            if (!)
            {
                _gameManagers.Add(loginRequest.SessionToken, new GameManager());
                var x = _gameManagers[loginRequest.SessionToken];
                x.SetTeamPlayerInfo(new List<LobbyPlayerInfo>
                {
                    new LobbyPlayerInfo()
                });
                x.SetGameInfo(new LobbyGameInfo
                {
                    GameConfig = new LobbyGameConfig
                    {
                        Map = "VR_Practice"
//                        Map = "CargoShip_Deathmatch"
//                        Map = "Casino01_Deathmatch"
//                        Map = "EvosLab_Deathmatch"
//                        Map = "Oblivion_Deathmatch"
//                        Map = "Reactor_Deathmatch"
//                        Map = "RobotFactory_Deathmatch"
//                        Map = "Skyway_Deathmatch"
                    }
                });
                x.LaunchGame();
            }

            return _gameManagers[loginRequest.SessionToken];
            */
        }

        public static void PlayerConnected(long accountId)
        {
            // We don't need to store this because the has player connected successfully
            _gameManagersByPlayerAccountID.Remove(accountId);
        }
    }
}
