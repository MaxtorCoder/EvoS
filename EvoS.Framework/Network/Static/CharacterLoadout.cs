using System;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(546)]
    public class CharacterLoadout : ICloneable
    {
        public CharacterLoadout(CharacterModInfo modInfo, CharacterAbilityVfxSwapInfo vfxInfo, string name = "",
            ModStrictness strictness = ModStrictness.AllModes)
        {
            LoadoutName = name;
            ModSet = modInfo;
            VFXSet = vfxInfo;
            Strictness = strictness;
        }

        public string LoadoutName { get; set; }

        public ModStrictness Strictness { get; set; }

        public CharacterModInfo ModSet { get; set; }

        public CharacterAbilityVfxSwapInfo VFXSet { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public const int c_maxLoadoutNameLength = 16;
    }
}
