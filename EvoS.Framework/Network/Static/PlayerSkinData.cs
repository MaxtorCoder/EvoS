using System;
using System.Collections.Generic;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(533)]
    public class PlayerSkinData
    {
        public PlayerSkinData()
        {
            Patterns = new List<PlayerPatternData>();
        }

        public bool Unlocked { get; set; }

        [EvosMessage(534)] public List<PlayerPatternData> Patterns { get; set; }
    }
}
