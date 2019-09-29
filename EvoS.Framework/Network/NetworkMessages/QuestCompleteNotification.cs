using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(263)]
    public class QuestCompleteNotification : WebSocketMessage
    {
        public int questId;
        public int rejectedCount;
    }
}
