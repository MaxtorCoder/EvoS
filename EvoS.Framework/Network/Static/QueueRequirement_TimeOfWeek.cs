using System;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(789)]
    public class QueueRequirement_TimeOfWeek : QueueRequirement
    {
        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }

        public override RequirementType Requirement => RequirementType.TimeOfWeek;

        public override bool AnyGroupMember => false;

        public static QueueRequirement Create(JsonReader reader)
        {
            reader.Read();
            string s = reader.Value as string;
            reader.Read();
            reader.Read();
            string s2 = reader.Value as string;
            reader.Read();
            return new QueueRequirement_TimeOfWeek
            {
                Start = TimeSpan.Parse(s),
                End = TimeSpan.Parse(s2)
            };
        }
    }
}
