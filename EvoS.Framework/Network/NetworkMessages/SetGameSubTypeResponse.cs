using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(50)]
    public class SetGameSubTypeResponse : WebSocketResponseMessage
    {
        public LocalizationPayload LocalizedFailure;
    }
}
