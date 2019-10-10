using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(380)]
    class GroupInviteResponse : WebSocketResponseMessage
    {
        public string FriendHandle;
        public LocalizationPayload LocalizedFailure;
    }
}
