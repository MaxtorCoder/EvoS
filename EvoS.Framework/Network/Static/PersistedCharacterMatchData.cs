using System;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(604)]
    public class PersistedCharacterMatchData : ICloneable
    {
        public PersistedCharacterMatchData()
        {
            SchemaVersion = new SchemaVersion<MatchSchemaChange>();
            MatchComponent = new MatchComponent();
            MatchDetailsComponent = new MatchDetailsComponent();
            MatchFreelancerStats = null;
        }

        [EvosMessage(605, ignoreGenericArgs: true)]
        public SchemaVersion<MatchSchemaChange> SchemaVersion { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public string GameServerProcessCode { get; set; }

        public MatchComponent MatchComponent { get; set; }

        public MatchDetailsComponent MatchDetailsComponent { get; set; }

        public MatchFreelancerStats MatchFreelancerStats { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
