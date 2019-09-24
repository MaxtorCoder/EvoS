using System;
using System.Collections.Generic;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(527, typeof(CharacterComponent))]
    public class CharacterComponent : ICloneable
    {
        public CharacterComponent()
        {
            this.Skins = new List<PlayerSkinData>();
            this.Taunts = new List<PlayerTauntData>();
            this.Mods = new List<PlayerModData>();
            this.AbilityVfxSwaps = new List<PlayerAbilityVfxSwapData>();
            this.LastSkin = default(CharacterVisualInfo);
            this.LastCards = default(CharacterCardInfo);
            this.LastMods = default(CharacterModInfo);
            this.LastRankedMods = default(CharacterModInfo);
            this.LastAbilityVfxSwaps = default(CharacterAbilityVfxSwapInfo);
            this.CharacterLoadouts = new List<CharacterLoadout>();
            this.CharacterLoadoutsRanked = new List<CharacterLoadout>();
            this.NumCharacterLoadouts = 0;
            this.LastSelectedLoadout = -1;
            this.LastSelectedRankedLoadout = -1;
        }

        public bool Unlocked { get; set; }

        public CharacterVisualInfo LastSkin { get; set; }

        public CharacterCardInfo LastCards { get; set; }

        public CharacterModInfo LastMods { get; set; }

        public CharacterModInfo LastRankedMods { get; set; }

        public CharacterAbilityVfxSwapInfo LastAbilityVfxSwaps { get; set; }

        public List<CharacterLoadout> CharacterLoadouts { get; set; }

        public List<CharacterLoadout> CharacterLoadoutsRanked { get; set; }

        public int LastSelectedLoadout { get; set; }

        public int LastSelectedRankedLoadout { get; set; }

        public int NumCharacterLoadouts { get; set; }

        [EvosMessage(531)]
        public List<PlayerSkinData> Skins { get; set; }

        [EvosMessage(528)]
        public List<PlayerTauntData> Taunts { get; set; }

        [EvosMessage(77)]
        public List<PlayerModData> Mods { get; set; }

        [EvosMessage(548)]
        public List<PlayerAbilityVfxSwapData> AbilityVfxSwaps { get; set; }

        public int ResetSelectionVersion { get; set; }

        public object Clone()
        {
            return base.MemberwiseClone();
        }
    }
}
