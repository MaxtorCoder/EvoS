using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(792)]
    public class QueueRequirement_MaxLeavingPoints : QueueRequirement
    {
        public float MaxValue { get; set; }

        public override bool AnyGroupMember => this.m_anyGroupMember;

        public override QueueRequirement.RequirementType Requirement =>
            QueueRequirement.RequirementType.MaxLeavingPoints;

        private bool m_anyGroupMember;
    }
}
