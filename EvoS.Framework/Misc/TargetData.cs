using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;

namespace EvoS.Framework.Misc
{
    [Serializable]
    public struct TargetData : ISerializedItem
    {
        public string Description;
        public float Range;
        public float MinRange;
        public bool CheckLineOfSight;
        public Ability.TargetingParadigm TargetingParadigm;

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            Description = stream.ReadString32();
            Range = stream.ReadSingle();
            MinRange = stream.ReadSingle();
            CheckLineOfSight = stream.ReadBoolean();
            stream.AlignTo();
            TargetingParadigm = (Ability.TargetingParadigm) stream.ReadInt32();
        }

        public override string ToString()
        {
            return $"{nameof(TargetData)}(" +
                   $"{nameof(Description)}: {Description}, " +
                   $"{nameof(Range)}: {Range}, " +
                   $"{nameof(MinRange)}: {MinRange}, " +
                   $"{nameof(CheckLineOfSight)}: {CheckLineOfSight}, " +
                   $"{nameof(TargetingParadigm)}: {TargetingParadigm}" +
                   ")";
        }
    }
}
