using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(256)]
    public class BadgeInfo
    {
        public int BadgeId;
    }
}
