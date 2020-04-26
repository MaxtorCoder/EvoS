using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Logging;
using EvoS.Framework.Misc;
using EvoS.Framework.Network.Game;
using EvoS.Framework.Network.Game.Messages;
using EvoS.Framework.Network.NetworkBehaviours;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.Unity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Game
{
    

    public class EvosServer
    {
        public NetworkServer NetworkServer;
        LobbyTeamInfo TeamInfo;
        LobbyGameInfo GameInfo;
        GameStatus GameStatus;

        GameEventManager GameEventManager = new GameEventManager();
        Dictionary<int, ActorData> ActorDataByPlayerId = new Dictionary<int, ActorData>();
        List<GameObject> SharedNetworkObjects = new List<GameObject>();
        Dictionary<Team, Stack<BoardSquare>> InitialSpawnPoints;

        public event Action<EvosServer> OnStop;

        public void Setup(LobbyGameInfo gameInfo, LobbyTeamInfo teamInfo)
        {
            GameInfo = gameInfo;
            TeamInfo = teamInfo;

            foreach (LobbyPlayerInfo playerInfo in teamInfo.TeamPlayerInfo)
            {
                CreateActor(playerInfo);
            }
        }

        public void HandleOnPlayerConnected(GameServerConnection connection)
        {
            // Set on disconnect event to notify this game server when a player loses conection
            connection.OnDisconnect += HandleOnPlayerDisconnect;

            // Spawn all shared network objects
            foreach (GameObject netObj in SharedNetworkObjects)
            {
                NetworkIdentity netId = netObj.GetComponent<NetworkIdentity>();
                netId.AddObserver(connection);
            }
        }


        /// <summary> Called by GameServerConnection when it disconnects </summary>
        public void HandleOnPlayerDisconnect(GameServerConnection connection)
        {
            Log.Print(LogType.Game, $"Player disconnected: {connection.ToString()}");

            // Remove this connection from the observers of each shared network object
            foreach (GameObject netObj in SharedNetworkObjects)
            {
                NetworkIdentity netId = netObj.GetComponent<NetworkIdentity>();
                netId.RemoveObserver(connection);
            }
        }

        public string GetRoomName()
        {
            return GameInfo.GameConfig.RoomName;
        }

        public ActorData GetActor(int playerId)
        {
            return this.ActorDataByPlayerId[playerId];
        }

        public void CreateActor(LobbyPlayerInfo playerInfo)
        {
            GameObject character = GameAssetsManager.GetCharacter(playerInfo.CharacterInfo.CharacterType);
            BoardSquare spawnBoardSquare;
            try
            {
                spawnBoardSquare = InitialSpawnPoints[playerInfo.TeamId].Pop();
            }
            catch (KeyNotFoundException)
            {
                throw new EvosException($"Spawnpoints not defined for map {GameInfo.GameConfig.Map}");
            }
            

            ActorData actorData = character.GetComponent<ActorData>();
            actorData.UpdateDisplayName(playerInfo.Handle);
            actorData.ServerLastKnownPosSquare = spawnBoardSquare;
            actorData.m_techPointsOnSpawn = 0; //energy
            

            PlayerData playerData = actorData.GetComponent<PlayerData>();
            playerData.m_playerHandle = playerInfo.Handle;
        }
    }
}
