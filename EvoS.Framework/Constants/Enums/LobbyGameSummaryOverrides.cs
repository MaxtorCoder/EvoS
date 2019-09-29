using System;
using System.Collections.Generic;

namespace EvoS.Framework.Constants.Enums
{
    [Serializable]
    public class LobbyGameSummaryOverrides
    {
        public Dictionary<long, int> GGPacksUsedList;
        public bool PlayWithFriendsBonus;
        public bool PlayedLastTurn;
    }
}
