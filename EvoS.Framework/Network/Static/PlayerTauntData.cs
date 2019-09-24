using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(530)]
    public class PlayerTauntData
    {
        public bool Unlocked { get; set; }
    }
}
