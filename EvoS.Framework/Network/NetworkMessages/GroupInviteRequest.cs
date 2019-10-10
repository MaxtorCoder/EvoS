using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(381)]
    public class GroupInviteRequest : WebSocketMessage
    {
        public string FriendHandle;
    }
}
