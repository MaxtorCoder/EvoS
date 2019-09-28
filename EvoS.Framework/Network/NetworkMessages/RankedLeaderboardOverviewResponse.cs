using System;
using System.Collections.Generic;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(117)]
    public class RankedLeaderboardOverviewResponse : WebSocketResponseMessage
    {
        public GameType GameType;
        [EvosMessage(102)] public Dictionary<int, PerGroupSizeTierInfo> TierInfoPerGroupSize;
        public LocalizationPayload LocalizedFailure;
    }
}
