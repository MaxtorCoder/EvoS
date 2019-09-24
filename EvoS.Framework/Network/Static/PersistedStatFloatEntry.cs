using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(41)]
    public class PersistedStatFloatEntry : ICloneable, IPersistedGameplayStat
    {
        public PersistedStatFloatEntry()
        {
            Sum = 0f;
            NumGamesInSum = 0;
            Min = 0f;
            Max = 0f;
        }

        public float Sum { get; set; }

        public int NumGamesInSum { get; set; }

        public float Min { get; set; }

        public float Max { get; set; }

        public float Average()
        {
            if (NumGamesInSum == 0)
            {
                return 0f;
            }

            return Sum / NumGamesInSum;
        }

        public void CombineStats(PersistedStatFloatEntry entry)
        {
            Sum += entry.Sum;
            NumGamesInSum += entry.NumGamesInSum;
            Max = Math.Max(Max, entry.Max);
            Min = Math.Min(Min, entry.Min);
        }

        public void Adjust(float val)
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

        public PersistedStatFloatEntry GetCopy()
        {
            return (PersistedStatFloatEntry) MemberwiseClone();
        }

        public float GetSum() => Sum;

        public float GetMin() => Min;

        public float GetMax() => Max;

        public int GetNumGames() => NumGamesInSum;
    }
}
