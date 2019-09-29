using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(411)]
    public class UseOverconRequest : WebSocketMessage
    {
        public int OverconId;
        public string OverconName;
        public int ActorId;
        public int Turn;
    }
}
