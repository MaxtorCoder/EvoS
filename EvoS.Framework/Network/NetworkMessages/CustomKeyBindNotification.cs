using System;
using System.Collections.Generic;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(95)]
    public class CustomKeyBindNotification : WebSocketMessage
    {
        public new static bool LogData = false;

        public Dictionary<int, KeyCodeData> CustomKeyBinds;
    }
}
