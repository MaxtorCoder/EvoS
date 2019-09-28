using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(751)]
    public class LeaveMatchmakingQueueRequest : WebSocketMessage
    {
    }
}
