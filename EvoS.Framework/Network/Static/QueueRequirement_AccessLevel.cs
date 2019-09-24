using System;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(797)]
    public class QueueRequirement_AccessLevel : QueueRequirement
    {
        public ClientAccessLevel AccessLevel { get; set; }

        public override bool AnyGroupMember => m_anyGroupMember;

        public override RequirementType Requirement => RequirementType.AccessLevel;

        public static QueueRequirement Create(JsonReader reader)
        {
            QueueRequirement_AccessLevel queueRequirement_AccessLevel = new QueueRequirement_AccessLevel();
            reader.Read();
            string value = reader.Value.ToString();
            queueRequirement_AccessLevel.AccessLevel =
                (ClientAccessLevel) Enum.Parse(typeof(ClientAccessLevel), value, true);
            reader.Read();
            if (reader.TokenType == JsonToken.PropertyName && reader.Value != null &&
                reader.Value.ToString() == "AnyGroupMember")
            {
                reader.Read();
                queueRequirement_AccessLevel.m_anyGroupMember = bool.Parse(reader.Value.ToString());
                reader.Read();
            }
            else
            {
                queueRequirement_AccessLevel.m_anyGroupMember = false;
            }

            return queueRequirement_AccessLevel;
        }

        private bool m_anyGroupMember;
    }
}
