using System;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(32)]
    public class PercentileInfo
    {
        [JsonIgnore]
        public bool HasData => AgainstSameFreelancer != null || MedianOfSameFreelancer != null || AgainstRole != null ||
                               MedianOfRole != null || AgainstAll != null || MedianOfAll != null ||
                               AgainstPeers != null || MedianOfPeers != null;

        public int? AgainstSameFreelancer;
        public int? AgainstRole;
        public int? AgainstAll;
        public int? AgainstPeers;
        public float? MedianOfSameFreelancer;
        public float? MedianOfRole;
        public float? MedianOfAll;
        public float? MedianOfPeers;
    }
}
