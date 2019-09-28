using System;
using System.Collections.Generic;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(552)]
    public class LobbyCharacterInfo
    {
        public LobbyCharacterInfo Clone()
        {
            return (LobbyCharacterInfo) base.MemberwiseClone();
        }

        public CharacterType CharacterType;

        public CharacterVisualInfo CharacterSkin = default(CharacterVisualInfo);

        public CharacterCardInfo CharacterCards = default(CharacterCardInfo);

        public CharacterModInfo CharacterMods = default(CharacterModInfo);

        public CharacterAbilityVfxSwapInfo CharacterAbilityVfxSwaps = default(CharacterAbilityVfxSwapInfo);

        [EvosMessage(528)] public List<PlayerTauntData> CharacterTaunts = new List<PlayerTauntData>();

        [EvosMessage(544)] public List<CharacterLoadout> CharacterLoadouts = new List<CharacterLoadout>();

        public int CharacterMatches;

        public int CharacterLevel;
    }
}
