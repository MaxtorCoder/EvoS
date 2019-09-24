using System;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(793)]
    public class QueueRequirement_GreaterThan : QueueRequirement
    {
        public int MinValue { get; set; }

        public override bool AnyGroupMember => this.m_anyGroupMember;

        public override QueueRequirement.RequirementType Requirement => this.m_requirementType;


        private QueueRequirement.RequirementType m_requirementType;

        private bool m_anyGroupMember;
    }
}
