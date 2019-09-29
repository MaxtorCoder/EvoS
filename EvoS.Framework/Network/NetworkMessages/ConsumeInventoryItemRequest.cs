using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(221)]
    public class ConsumeInventoryItemRequest : WebSocketMessage
    {
        public int ItemId;
        public int ItemCount;
        public bool ToISO;
    }
}
