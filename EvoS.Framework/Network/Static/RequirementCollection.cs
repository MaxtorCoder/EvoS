using System;
using System.Collections.Generic;
using NetSerializer;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.Static
{
    [JsonConverter(typeof(JsonConverter))]
    [Serializable]
    [EvosMessage(147)]
    public class RequirementCollection // : IEnumerable<QueueRequirement>, IEnumerable
    {
        [EvosMessage(150)]
        public List<byte[]> RequirementsAsBinaryData;

        [NonSerialized] private bool m_dirty = true;

        [EvosMessage(148)]
        private List<QueueRequirement> m_queueRequirementAsList = new List<QueueRequirement>();

        private static Serializer s_serializer;

        private class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(RequirementCollection);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                JsonSerializer serializer)
            {
                throw new NotImplementedException("Not copied");
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException("Not copied");
            }
        }
    }
}
