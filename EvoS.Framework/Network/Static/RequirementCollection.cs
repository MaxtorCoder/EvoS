using System;
using System.Collections.Generic;
using NetSerializer;

namespace EvoS.Framework.Network.Static
{
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
    }
}
