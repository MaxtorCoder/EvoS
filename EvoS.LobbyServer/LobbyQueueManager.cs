using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.LobbyServer
{
    class LobbyQueueManager
    {
        private static LobbyQueueManager Instance = null;
        private Dictionary<GameType, List<ClientConnection>> Queue;

        private LobbyQueueManager() {
            Queue = new Dictionary<GameType, List<ClientConnection>>();
        }
        

        private static LobbyQueueManager GetInstance() {
            if (Instance == null) {
                Instance = new LobbyQueueManager();
            }
            return Instance;
        }

        public static void AddToQueue(GameType queueType, ClientConnection client)
        {
            GetInstance();
            if (!Instance.Queue.ContainsKey(queueType))
            {
                Instance.Queue.Add(queueType, new List<ClientConnection>());
            }
            Instance.Queue[queueType].Add(client);
        }

        
    }
}
