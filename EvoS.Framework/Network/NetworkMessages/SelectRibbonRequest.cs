using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(400)]
    public class SelectRibbonRequest : WebSocketMessage
    {
        public int RibbonID;
    }
}
