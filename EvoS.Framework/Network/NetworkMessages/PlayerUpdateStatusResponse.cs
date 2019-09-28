using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(351)]
    public class PlayerUpdateStatusResponse : WebSocketResponseMessage
    {
        public long AccountId;
        public string StatusString;
    }
}
