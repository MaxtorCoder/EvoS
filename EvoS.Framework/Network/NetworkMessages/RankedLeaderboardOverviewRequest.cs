using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(118)]
    public class RankedLeaderboardOverviewRequest : WebSocketMessage
    {
        public GameType GameType;
    }
}
