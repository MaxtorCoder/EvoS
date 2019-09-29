using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(374)]
    public class GroupConfirmationRequest : WebSocketMessage
    {
        public long GroupId;
        public string LeaderName;
        public string LeaderFullHandle;
        public string JoinerName;
        public long JoinerAccountId;
        public long ConfirmationNumber;
        public TimeSpan ExpirationTime;
        public GroupConfirmationRequest.JoinType Type;

        [EvosMessage(375)]
        public enum JoinType
        {
            Unicode001D,
            Unicode000E
        }
    }
}
