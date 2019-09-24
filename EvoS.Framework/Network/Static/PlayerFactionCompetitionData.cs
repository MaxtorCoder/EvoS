using System;
using System.Collections.Generic;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(633)]
    public class PlayerFactionCompetitionData
    {
        public int CompetitionID;
        [EvosMessage(634)]
        public Dictionary<int, FactionPlayerData> Factions;
    }
}
