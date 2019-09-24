using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(539)]
    public class PlayerColorData
    {
        public bool Unlocked { get; set; }

        public PlayerColorData GetDeepCopy()
        {
            return MemberwiseClone() as PlayerColorData;
        }
    }
}
