using EvoS.Framework.Network.NetworkMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentralServer.LobbyServer.Quest
{
    class QuestManager
    {
        public static LobbySeasonQuestDataNotification GetSeasonQuestDataNotification()
        {
            // TODO
            return new LobbySeasonQuestDataNotification()
            {
                SeasonChapterQuests = new Dictionary<int, EvoS.Framework.Network.Static.SeasonChapterQuests>()
            };
        }
    }
}
