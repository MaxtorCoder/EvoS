using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(349)]
    public class PaymentMethodsRequest : WebSocketMessage
    {
    }
}
