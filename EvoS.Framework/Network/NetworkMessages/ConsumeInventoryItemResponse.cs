using System;
using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(220)]
    public class ConsumeInventoryItemResponse : WebSocketResponseMessage
    {
        public ConsumeInventoryItemResult Result;
        [EvosMessage(212)]
        public List<InventoryItemWithData> OutputItems;
    }
}
