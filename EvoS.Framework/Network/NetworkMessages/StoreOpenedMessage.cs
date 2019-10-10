using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network.NetworkMessages
{
    /*
    * Sent by the player when it opens the Collections tab from the main menu
    * note: Have no idea what response/notification send to this
    */
    [Serializable]
    [EvosMessage(350)]
    public class StoreOpenedMessage : WebSocketMessage
    {
        
    }
}
