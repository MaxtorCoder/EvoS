using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(754)]
    public class LeaveGameResponse : WebSocketResponseMessage
    {
    }
}
