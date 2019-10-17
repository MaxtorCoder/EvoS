using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    public class AbilityStatMod : ISerializedItem
    {
        public StatType stat;
        public ModType modType;
        public float modValue;

        public AbilityStatMod GetShallowCopy()
        {
            return (AbilityStatMod) MemberwiseClone();
        }

        public override string ToString()
        {
            return "[" + stat + "] " + modType + " " + modValue;
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            stat = (StatType) stream.ReadInt32();
            modType = (ModType) stream.ReadInt32();
            modValue = stream.ReadSingle();
        }

        public string GetOperationsString()
        {
            return
                $"{stat.ToString()} {(modType != ModType.Multiplier ? ((double) modValue < 0.0 ? '-' : '+') : 'x')}{(modType == ModType.BaseAdd || modType == ModType.BonusAdd ? ((int) Math.Abs(modValue)).ToString() : modValue.ToString("F2"))}{(modType != ModType.BonusAdd ? (modType != ModType.BaseAdd ? (modType != ModType.PercentAdd ? char.MinValue : 'x') : '_') : '^')}";
        }
    }
}
