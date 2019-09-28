using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(752)]
    public class JoinMatchmakingQueueResponse : WebSocketResponseMessage
    {
        public LocalizationPayload LocalizedFailure;
    }
}
