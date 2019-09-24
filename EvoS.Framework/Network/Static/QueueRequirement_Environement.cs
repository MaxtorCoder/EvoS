using System;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(794)]
    public class QueueRequirement_Environement : QueueRequirement
    {
        public override bool AnyGroupMember => false;

        public override RequirementType Requirement => RequirementType.ProhibitEnvironment;
        
        private EnvironmentType Environment;
    }
}
