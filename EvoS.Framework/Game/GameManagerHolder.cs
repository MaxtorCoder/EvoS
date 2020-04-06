using System.Collections.Generic;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.Game.Messages;
using EvoS.Framework.Network.NetworkMessages;
using EvoS.Framework.Network.Static;

namespace EvoS.Framework.Game
{
    public class GameManagerHolder
    {
        private static Dictionary<string, GameManager> _gameManagers = new Dictionary<string, GameManager>();
        private static Dictionary<long, GameManager> _gameManagersBySessionToken = new Dictionary<long, GameManager>();

        class EmptyRoomNameGameServerException : System.Exception { }
        class InvalidPlayerAccountIDGameServerException : System.Exception { }

        /// <summary>
        /// Creates a GameManager instance which holds each match. Called from the lobby queue
        /// </summary>
        /// <param name="gameInfo">Match configuration</param>
        /// <param name="teamInfo">players information (players + bots)</param>
        public static void CreateGameManager(LobbyGameInfo gameInfo, LobbyTeamInfo teamInfo, List<long> PlayerSessionTokens)
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

                        _gameManagersBySessionToken.Add(player.AccountId, gameManager);
                    }
                }

                foreach (long sessionToken in PlayerSessionTokens) {
                    _gameManagersBySessionToken.Add(sessionToken, gameManager);
                }

                _gameManagers.Add(gameInfo.GameConfig.RoomName, gameManager);
                gameManager.LaunchGame();
                Log.Print(LogType.Debug, "Game Server Launched with name " + gameInfo.GameConfig.RoomName);
            }
            catch (System.Exception e)
            {
                throw new EvosException("Error Creating Game Manager", e);
            }
        }

        public static GameManager? FindGameManager(long sessionToken)
        {
            try
            {
                return _gameManagersBySessionToken[sessionToken];
            }
            catch (KeyNotFoundException)
            {
                Log.Print(LogType.Debug, "Available gameManagers:");
                foreach (var a in _gameManagers)
                {
                    Log.Print(LogType.Debug, $"{a.Key}: {a.Value.GameConfig.RoomName}");
                }

                Log.Print(LogType.Debug, "Available gameManagers per account:");
                foreach (var a in _gameManagersBySessionToken)
                {
                    Log.Print(LogType.Debug, $"{a.Key}: {a.Value.GameConfig.RoomName}");
                }
                return null;
            }
        }

        public static void PlayerConnected(long accountId)
        {
            //_gameManagersByPlayerAccountID.Remove(accountId); // do not actually remove the player, if it fails on loading when it tries to reconnect it sends a LoginRequest again and we need the data
        }
    }
}
