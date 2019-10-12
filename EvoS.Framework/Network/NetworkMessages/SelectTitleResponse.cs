using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(403)]
    public class SelectTitleResponse : WebSocketResponseMessage
    {
        public int CurrentTitleID;
    }
}
