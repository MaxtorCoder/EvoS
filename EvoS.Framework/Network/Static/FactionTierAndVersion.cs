using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(585)]
    public class FactionTierAndVersion
    {
        public int Tier;
        public int Version;
    }
}
