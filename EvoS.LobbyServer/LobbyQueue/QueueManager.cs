using EvoS.LobbyServer.LobbyQueue;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.LobbyServer
{
    class QueueManager
    {
        private static QueueManager Instance = null;
        private Dictionary<GameType, List<QueueParticipant>> Queue;

        private QueueManager() {
            Queue = new Dictionary<GameType, List<QueueParticipant>>();
        }
        

        private static QueueManager GetInstance() {
            if (Instance == null) {
                Instance = new QueueManager();
            }
            return Instance;
        }

        public static void AddPlayerToQueue(GameType gameType, ClientConnection client)
        {
            GetInstance();
            if (!Instance.Queue.ContainsKey(gameType))
            {
                Instance.Queue.Add(gameType, new List<QueueParticipant>());
            }
            QueueParticipant player = new QueuePlayer(client);
            Instance.Queue[gameType].Add(player);
        }

        public static void RemovePlayerFromQueues(ClientConnection client)
        {
            foreach (var queueType in Instance.Queue.Keys)
            {
                foreach (var participant in Instance.Queue[queueType])
                {
                    if (participant.IsGroup())
                    {

                    }
                }
            }
        }

        public static void ProcessMatchmaking(GameType gameType)
        {
            // TODO
        }

        public static void RemovePlayerFromQueue()
        {
            // TODO
        }
    }
}
