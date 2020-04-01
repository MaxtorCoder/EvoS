using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(343)]
    public class PricesRequest : WebSocketMessage
    {
        [NonSerialized]
        public new static bool LogData = false;
    }
}
