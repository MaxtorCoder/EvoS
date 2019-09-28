using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(352)]
    public class PlayerUpdateStatusRequest : WebSocketMessage
    {
        public long AccountId;
        public string StatusString;
    }
}
