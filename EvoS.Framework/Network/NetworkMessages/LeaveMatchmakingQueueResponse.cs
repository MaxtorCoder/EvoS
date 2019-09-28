using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(750)]
    public class LeaveMatchmakingQueueResponse : WebSocketResponseMessage
    {
    }
}
