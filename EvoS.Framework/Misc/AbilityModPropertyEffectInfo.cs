using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("AbilityModPropertyEffectInfo")]
    public class AbilityModPropertyEffectInfo : ISerializedItem
    {
        public ModOp operation;
        public bool useSequencesFromSource;
        public StandardEffectInfo effectInfo;

        public AbilityModPropertyEffectInfo()
        {
        }

        public AbilityModPropertyEffectInfo(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            operation = (ModOp) stream.ReadInt32();
            useSequencesFromSource = stream.ReadBoolean();
            stream.AlignTo();
            effectInfo = new StandardEffectInfo(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(AbilityModPropertyEffectInfo)}>(" +
                   $"{nameof(operation)}: {operation}, " +
                   $"{nameof(useSequencesFromSource)}: {useSequencesFromSource}, " +
                   $"{nameof(effectInfo)}: {effectInfo}, " +
                   ")";
        }

        public enum ModOp
        {
            Ignore,
            Override,
        }
    }
}
