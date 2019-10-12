using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.NetworkMessages;
using EvoS.Framework.Network.Static;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvoS.LobbyServer
{
    class QueueManager
    {
        private static QueueManager Instance = null;
        private List<ClientConnection> Queue;
        private static Dictionary<GameType, int>QueuedPlayers;

        private QueueManager() {
            Queue = new List<ClientConnection>();
            QueuedPlayers = new Dictionary<GameType, int>(){{GameType.PvE, 0},{GameType.PvP, 0}};
        }
        

        public static QueueManager GetInstance() {
            if (Instance == null) {
                Instance = new QueueManager();
            }
            return Instance;
        }

        public static void AddPlayerToQueue(ClientConnection client)
        {
            QueuedPlayers[client.SelectedGameType] += 1;
            Instance.Queue.Add(client);

            ProcessMatchmaking();
        }

        public static void RemovePlayerFromQueue(ClientConnection client)
        {
            QueuedPlayers[client.SelectedGameType] -= 1;
            Instance.Queue.Remove(client);
            ProcessMatchmaking();
        }

        public async static void ProcessMatchmaking()
        {
            await SendNotification();
        }

        private async static Task SendNotification()
        {
            foreach (var client in Instance.Queue)
            {
                MatchmakingQueueAssignmentNotification notification = new MatchmakingQueueAssignmentNotification()
                {
                    Reason = "",
                    MatchmakingQueueInfo = new LobbyMatchmakingQueueInfo()
                    {
                        ShowQueueSize = true,
                        QueuedPlayers = QueuedPlayers[client.SelectedGameType],
                        AverageWaitTime = TimeSpan.FromSeconds(100),
                        PlayersPerMinute = 0,
                        GameConfig = new LobbyGameConfig(),
                        QueueStatus = QueueStatus.QueueDoesntHaveEnoughHumans
                    }
                };

                await client.SendMessage(notification);
            }
        }
    }
}
