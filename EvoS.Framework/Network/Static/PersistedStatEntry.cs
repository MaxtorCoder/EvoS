using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(42)]
    public class PersistedStatEntry : ICloneable, IPersistedGameplayStat
    {
        public PersistedStatEntry()
        {
            Sum = 0;
            NumGamesInSum = 0;
            Min = 0;
            Max = 0;
        }

        public int Sum { get; set; }

        public int NumGamesInSum { get; set; }

        public int Min { get; set; }

        public int Max { get; set; }

        public float Average()
        {
            if (NumGamesInSum == 0)
            {
                return 0f;
            }

            return Sum / (float) NumGamesInSum;
        }

        public void CombineStats(PersistedStatEntry entry)
        {
            Sum += entry.Sum;
            NumGamesInSum += entry.NumGamesInSum;
            Max = Math.Max(Max, entry.Max);
            Min = Math.Min(Min, entry.Min);
        }

        public void Adjust(int val)
        {
            bool flag = NumGamesInSum == 0;
            Sum += val;
            NumGamesInSum++;
            if (val > Max || flag)
            {
                Max = val;
            }

            if (val < Min || flag)
            {
                Min = val;
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public PersistedStatEntry GetCopy()
        {
            return (PersistedStatEntry) MemberwiseClone();
        }

        public float GetSum() => Sum;

        public float GetMin() => Min;

        public float GetMax() => Max;

        public int GetNumGames() => NumGamesInSum;
    }
}
