using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(435)]
    public class UIActionNotification : WebSocketMessage
    {
        public string Context;
    }
}
