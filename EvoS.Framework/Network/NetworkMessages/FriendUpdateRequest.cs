using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(364)]
    public class FriendUpdateRequest : WebSocketMessage
    {
        public string FriendHandle;
        public long FriendAccountId;
        public FriendOperation FriendOperation;
        public string StringData;
    }
}
