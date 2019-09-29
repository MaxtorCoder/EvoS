using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(737)]
    public class TierPlacement
    {
        public int Tier;
        public float Points;
    }
}
