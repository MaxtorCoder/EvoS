using System;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(372)]
    public class GroupConfirmationResponse : WebSocketResponseMessage
    {
        public long GroupId;
        public GroupInviteResponseType Acceptance;
        public long ConfirmationNumber;
        public long JoinerAccountId;
    }
}
