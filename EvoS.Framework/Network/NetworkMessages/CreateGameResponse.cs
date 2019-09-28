using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(758)]
    public class CreateGameResponse : WebSocketResponseMessage
    {
        public LocalizationPayload LocalizedFailure;
        public bool AllowRetry;
    }
}
