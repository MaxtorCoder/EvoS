using System;
using System.Collections.Generic;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(428)]
    public class FactionCompetitionNotification : WebSocketMessage
    {
        public int ActiveIndex;
        [EvosMessage(429)]
        public Dictionary<int, long> Scores;
    }
}
