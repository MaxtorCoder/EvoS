using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(362)]
    public class FriendUpdateResponse : WebSocketResponseMessage
    {
        public long FriendAccountId;
        public string FriendHandle;
        public FriendOperation FriendOperation;
        public LocalizationPayload LocalizedFailure;
    }
}
