using System;
using System.Collections.Generic;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(105)]
    public struct PerGroupSizeTierInfo
    {
        [EvosMessage(106)] public Dictionary<int, RankedTierInfo> PerTierInfo;
        [EvosMessage(110)] public RankedScoreboardEntry? OurEntry;
    }
}
