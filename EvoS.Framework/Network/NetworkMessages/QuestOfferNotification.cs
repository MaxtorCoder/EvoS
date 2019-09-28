using System;
using System.Collections.Generic;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(269)]
    public class QuestOfferNotification : WebSocketMessage
    {
        public bool OfferDailyQuest;
        public List<int> DailyQuestIds;
        public Dictionary<int, int> RejectedQuestCount;
    }
}
