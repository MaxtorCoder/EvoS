using System;
using System.Collections.Generic;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(95)]
    public class CustomKeyBindNotification : WebSocketMessage
    {
        public Dictionary<int, KeyCodeData> CustomKeyBinds;
    }
}
