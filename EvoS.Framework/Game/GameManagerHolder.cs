using System.Collections.Generic;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.Game;
using EvoS.Framework.Network.Game.Messages;
using EvoS.Framework.Network.NetworkMessages;
using EvoS.Framework.Network.Static;

namespace EvoS.Framework.Game
{
    public class GameManagerHolder
    {
        private static Dictionary<string, EvosServer> ActiveServers = new Dictionary<string, EvosServer>();
        private static Dictionary<long, EvosServer> ServersByPlayerSessionToken = new Dictionary<long, EvosServer>();

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
                EvosServer server = new EvosServer();
                server.Setup(gameInfo, teamInfo);
                ActiveServers.Add(gameInfo.GameConfig.RoomName, server);
                foreach (long sessionToken in PlayerSessionTokens)
                {
                    ServersByPlayerSessionToken.Add(sessionToken, server);
                }

                server.OnStop += HandleOnServerStop;
                
                Log.Print(LogType.Debug, "Game Server Launched with name " + gameInfo.GameConfig.RoomName);
            }
            catch (System.Exception e)
            {
                throw new EvosException("Error Creating Game Manager", e);
            }
        }

        /// <summary> Removes from memory the references to the server </summary>
        public static void HandleOnServerStop(EvosServer server)
        {
            ActiveServers.Remove(server.GetRoomName());
            foreach (long sessionToken in ServersByPlayerSessionToken.Keys)
            {
                if (ServersByPlayerSessionToken[sessionToken] == server)
                {
                    ServersByPlayerSessionToken.Remove(sessionToken);
                }
            }
        }

        /// <summary> Assigns the server to a connection and sets the events </summary>
        public static void AssignServer(GameServerConnection connection)
        {
            EvosServer server;

            if (ServersByPlayerSessionToken.TryGetValue(connection.SessionToken, out server))
            {
                connection.Server = server;
                server.HandleOnPlayerConnected(connection);
                Log.Print(LogType.Game, $"{connection.ToString()} assigned to server {server.GetRoomName()}");
            }
            else
            {
                Log.Print(LogType.Error, $"No server found for player {connection.ToString()}");
                connection.Disconnect();
            }
        }

    }
}
