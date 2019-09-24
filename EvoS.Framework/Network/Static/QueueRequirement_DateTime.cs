using System;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(795)]
    public class QueueRequirement_DateTime : QueueRequirement
    {
        public override bool AnyGroupMember => false;

        public override QueueRequirement.RequirementType Requirement => this.m_requirementType;

        public static QueueRequirement Create(QueueRequirement.RequirementType reqType, JsonReader reader)
        {
            QueueRequirement_DateTime queueRequirement_DateTime = new QueueRequirement_DateTime();
            queueRequirement_DateTime.m_requirementType = reqType;
            reader.Read();
            if (reader.TokenType == JsonToken.Date)
            {
                queueRequirement_DateTime.m_dateTime = (DateTime) reader.Value;
                reader.Read();
            }
            else
            {
                string s = reader.Value as string;
                queueRequirement_DateTime.m_dateTime = DateTime.Parse(s);
                reader.Read();
            }

            return queueRequirement_DateTime;
        }

        private QueueRequirement.RequirementType m_requirementType;

        private DateTime m_dateTime;
    }
}
