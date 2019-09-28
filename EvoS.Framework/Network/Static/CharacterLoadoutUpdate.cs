using System;
using System.Collections.Generic;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(725)]
    public struct CharacterLoadoutUpdate
    {
        [EvosMessage(544)]
        public List<CharacterLoadout> CharacterLoadoutChanges;
    }

}
