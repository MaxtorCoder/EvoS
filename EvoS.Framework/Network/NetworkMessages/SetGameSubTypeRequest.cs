using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(51)]
    public class SetGameSubTypeRequest : WebSocketMessage
    {
        public GameType gameType;
        public ushort SubTypeMask;
    }
}
