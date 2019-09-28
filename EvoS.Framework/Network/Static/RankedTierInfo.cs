using System;
using System.Collections.Generic;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(109)]
    public struct RankedTierInfo
    {
        public List<int> Instances;
    }
}
