using System;
using System.Collections.Generic;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(138)]
    public class FreelancerSet
    {
        [EvosMessage(139)]
        public List<CharacterType> Types;
        [EvosMessage(141)]
        public List<CharacterRole> Roles;
        [EvosMessage(10)]
        public List<int> FactionGroups;
    }
}
