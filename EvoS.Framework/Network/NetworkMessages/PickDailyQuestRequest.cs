using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(267)]
    public class PickDailyQuestRequest : WebSocketMessage
    {
        public int questId;
    }
}
