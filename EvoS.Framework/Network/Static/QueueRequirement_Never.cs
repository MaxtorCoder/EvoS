using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(790)]
    public class QueueRequirement_Never : QueueRequirement
    {
        public override bool AnyGroupMember => false;

        public override RequirementType Requirement => m_requirementType;

        public static QueueRequirement CreateAdminDisabled()
        {
            return new QueueRequirement_Never
            {
                m_requirementType = RequirementType.AdminDisabled
            };
        }

        private RequirementType m_requirementType = RequirementType.AdminDisabled;
    }
}
