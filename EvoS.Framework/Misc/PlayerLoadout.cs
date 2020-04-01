using EvoS.Framework.Network.Static;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Misc
{
    public class PlayerLoadout
    {
        public String Name;
        public CharacterVisualInfo Skin;
        public CharacterModInfo Mods;
        public CharacterCardInfo Cards;
        public CharacterAbilityVfxSwapInfo AbilityVfxSwaps;
        public List<PlayerTauntData> Taunts;

        public PlayerLoadout()
        {
            Name = "default";
            Skin = new CharacterVisualInfo(0, 0, 0);
            Mods.Reset();
            Cards.Reset();
            AbilityVfxSwaps.Reset();
            Taunts = new List<PlayerTauntData>();
        }
    }
}
