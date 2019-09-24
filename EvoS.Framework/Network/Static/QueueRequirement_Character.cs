using System;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(796)]
    public class QueueRequirement_Character : QueueRequirement
    {
        public override RequirementType Requirement => m_requirementType;

        public override bool AnyGroupMember => m_anyGroupMember;

        public static QueueRequirement Create(RequirementType reqType, JsonReader reader)
        {
            QueueRequirement_Character queueRequirement_Character = new QueueRequirement_Character();
            queueRequirement_Character.m_requirementType = reqType;
            reader.Read();
            string value = reader.Value.ToString();
            queueRequirement_Character.CharacterType = (CharacterType) Enum.Parse(typeof(CharacterType), value, true);
            reader.Read();
            if (reader.TokenType == JsonToken.PropertyName && reader.Value != null &&
                reader.Value.ToString() == "AnyGroupMember")
            {
                reader.Read();
                queueRequirement_Character.m_anyGroupMember = bool.Parse(reader.Value.ToString());
                reader.Read();
            }
            else
            {
                queueRequirement_Character.m_anyGroupMember = false;
            }

            return queueRequirement_Character;
        }

        private CharacterType CharacterType;

        private RequirementType m_requirementType =
            RequirementType.HasUnlockedCharacter;

        private bool m_anyGroupMember;
    }
}
