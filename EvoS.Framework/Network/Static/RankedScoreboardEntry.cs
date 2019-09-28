using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(111)]
    public struct RankedScoreboardEntry : IComparable<RankedScoreboardEntry>
    {
        public int CompareTo(RankedScoreboardEntry other)
        {
            bool flag = Tier <= 0;
            bool flag2 = other.Tier <= 0;
            if (flag != flag2)
            {
                return (!flag) ? -1 : 1;
            }

            bool flag3 = Tier == 1 || Tier == 2;
            bool flag4 = other.Tier == 1 || other.Tier == 2;
            if (flag3 != flag4)
            {
                return (!flag3) ? 1 : -1;
            }

            if (!flag3 && Tier != other.Tier)
            {
                return (Tier >= other.Tier) ? 1 : -1;
            }

            if (TierPoints != other.TierPoints)
            {
                return (TierPoints <= other.TierPoints) ? 1 : -1;
            }

            if (WinStreak != other.WinStreak)
            {
                return (WinStreak <= other.WinStreak) ? 1 : -1;
            }

            if (WinCount != other.WinCount)
            {
                return (WinCount <= other.WinCount) ? 1 : -1;
            }

            if (MatchCount != other.MatchCount)
            {
                return (MatchCount >= other.MatchCount) ? 1 : -1;
            }

            return other.LastMatch.CompareTo(LastMatch);
        }

        public string Handle;
        public DateTime LastMatch;
        public long AccountID;
        public int Tier;
        public float TierPoints;
        public int WinCount;
        public int WinStreak;
        public int MatchCount;
        public int InstanceId;
        public int YesterdaysTier;
        public int YesterdaysPoints;
    }
}
