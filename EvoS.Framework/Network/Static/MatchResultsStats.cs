using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(607)]
    public class MatchResultsStats : ICloneable
    {
        public object Clone()
        {
            return base.MemberwiseClone();
        }

        [EvosMessage(608)]
        public MatchResultsStatline[] FriendlyStatlines;
        public MatchResultsStatline[] EnemyStatlines;
        public int RedScore;
        public int BlueScore;
        public float GameTime;
        public string VictoryCondition;
        public int VictoryConditionTurns;
    }
}
