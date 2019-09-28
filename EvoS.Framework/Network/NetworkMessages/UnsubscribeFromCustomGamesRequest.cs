using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(762)]
    public class UnsubscribeFromCustomGamesRequest : WebSocketMessage
    {
    }
}
