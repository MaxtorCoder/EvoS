using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(741)]
    public class ForbiddenDevKnowledge
    {
        public int AccountLevel;
        public int TotalLevel;
        public int NumWins;
        public float UsedMatchmakingElo;
        public float AccMatchmakingElo;
        public int AccMatchmakingCount;
        public float CharMatchmakingElo;
        public int CharMatchmakingCount;
        public long GroupIdAtStartOfMatch;
        public int GroupSizeAtStartOfMatch;
        public TierPlacement TierChangeMin;
        public TierPlacement TierChangeMax;
        public TierPlacement TierCurrent;
    }
}
