using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(569)]
    public class QuestMetaData
    {
//        [JsonIgnore]
//        public DateTime UtcFirstCompleted =>
//            (!UtcCompletedTimes.IsNullOrEmpty<DateTime>())
//                ? UtcCompletedTimes.FirstOrDefault<DateTime>()
//                : DateTime.MinValue;
//
//        [JsonIgnore]
//        public DateTime UtcLastCompleted =>
//            (!UtcCompletedTimes.IsNullOrEmpty<DateTime>())
//                ? UtcCompletedTimes.LastOrDefault<DateTime>()
//                : DateTime.MinValue;

        public int CompletedCount;

        public int RejectedCount;

        public int AbandonedCount;

        public int Weight;

        [EvosMessage(570)]
        public List<DateTime> UtcCompletedTimes;

        public DateTime UtcCompleted;

        public DateTime? PstAbandonDate;
    }
}
