using System;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(357)]
    public class FriendStatusNotification : WebSocketMessage
    {
        public FriendList FriendList;
    }
}
