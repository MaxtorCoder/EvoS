using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(399)]
    public class SelectRibbonResponse : WebSocketResponseMessage
    {
        public int CurrentRibbonID;
    }
}
