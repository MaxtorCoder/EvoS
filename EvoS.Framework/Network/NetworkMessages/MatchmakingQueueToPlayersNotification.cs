using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(180)]
    public class MatchmakingQueueToPlayersNotification : WebSocketMessage
    {
        public long AccountId;
        public MatchmakingQueueMessage MessageToSend;
        public GameType GameType;
        public ushort SubTypeMask;

        [EvosMessage(181)]
        public enum MatchmakingQueueMessage
        {
            Unicode001D,
            Unicode000E,
            Unicode0012,
            Unicode0015
        }
    }
}
