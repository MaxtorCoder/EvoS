using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(487)]
    public class PreviousGameInfoRequest : WebSocketMessage
    {
    }
}
