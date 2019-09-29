using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(432)]
    public class UseGGPackRequest : WebSocketResponseMessage
    {
    }
}
