using System;
using System.Collections.Generic;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(2)]
    public class LobbySeasonQuestDataNotification : WebSocketMessage
    {
        [EvosMessage(3)]
        public Dictionary<int, SeasonChapterQuests> SeasonChapterQuests;
    }
}
