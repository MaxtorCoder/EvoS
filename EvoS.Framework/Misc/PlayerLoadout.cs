using EvoS.Framework.Network.Static;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Misc
{
    public class PlayerLoadout
    {
        public CharacterVisualInfo Skin;
        public CharacterModInfo Mods;
        public CharacterCardInfo Cards;
        public CharacterAbilityVfxSwapInfo AbilityVfxSwaps;

        public PlayerLoadout()
        {
            Skin = new CharacterVisualInfo(0, 0, 0);
            Mods.Reset();
            Cards.Reset();
            AbilityVfxSwaps.Reset();
        }
    }
}
