using System;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(735)]
    public class GameStatusNotification : WebSocketMessage
    {
        public string GameServerProcessCode;
        public GameStatus GameStatus;
    }
}
