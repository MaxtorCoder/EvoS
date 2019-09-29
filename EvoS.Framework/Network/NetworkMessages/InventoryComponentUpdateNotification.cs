using System;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(490)]
    public class InventoryComponentUpdateNotification : WebSocketResponseMessage
    {
        public InventoryComponent InventoryComponent;
    }
}
