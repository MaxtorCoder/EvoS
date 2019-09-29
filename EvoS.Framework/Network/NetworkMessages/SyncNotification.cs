using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(760)]
    public class SyncNotification : WebSocketMessage
    {
        public bool Reply;
    }
}
