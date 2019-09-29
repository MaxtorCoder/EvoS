using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(410)]
    public class UseOverconResponse : WebSocketResponseMessage
    {
        public int OverconId;
        public int ActorId;
    }
}
