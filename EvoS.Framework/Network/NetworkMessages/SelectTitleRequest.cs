using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(404)]
    public class SelectTitleRequest : WebSocketMessage
    {
        public int TitleID;
    }
}
