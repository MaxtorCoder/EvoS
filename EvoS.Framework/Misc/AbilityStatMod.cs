using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    public class AbilityStatMod : ISerializedItem
    {
        public StatType Stat;
        public ModType ModType;
        public float ModValue;

        public AbilityStatMod GetShallowCopy()
        {
            return (AbilityStatMod) MemberwiseClone();
        }

        public override string ToString()
        {
            return "[" + Stat + "] " + ModType + " " + ModValue;
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            Stat = (StatType) stream.ReadInt32();
            ModType = (ModType) stream.ReadInt32();
            ModValue = stream.ReadSingle();
        }

        public string GetOperationsString()
        {
            return
                $"{Stat.ToString()} {(ModType != ModType.Multiplier ? ((double) ModValue < 0.0 ? '-' : '+') : 'x')}{(ModType == ModType.BaseAdd || ModType == ModType.BonusAdd ? ((int) Math.Abs(ModValue)).ToString() : ModValue.ToString("F2"))}{(ModType != ModType.BonusAdd ? (ModType != ModType.BaseAdd ? (ModType != ModType.PercentAdd ? char.MinValue : 'x') : '_') : '^')}";
        }
    }
}
