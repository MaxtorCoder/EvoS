using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [EvosMessage(266)]
    [Serializable]
    public class PickDailyQuestResponse : WebSocketResponseMessage
    {
    }
}
