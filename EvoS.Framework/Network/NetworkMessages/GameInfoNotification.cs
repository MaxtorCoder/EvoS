using System;
using System.Collections.Generic;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(736)]
    public class GameInfoNotification : WebSocketMessage
    {
        public LobbyGameInfo GameInfo;
        public LobbyTeamInfo TeamInfo;
        public LobbyPlayerInfo PlayerInfo;
        public TierPlacement TierCurrent;
        public TierPlacement TierChangeMin;
        public TierPlacement TierChangeMax;
        [EvosMessage(738)]
        public Dictionary<int, ForbiddenDevKnowledge> DevOnly;
    }

}
