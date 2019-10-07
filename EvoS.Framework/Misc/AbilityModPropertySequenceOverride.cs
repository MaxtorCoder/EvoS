using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("AbilityModPropertySequenceOverride")]
    public class AbilityModPropertySequenceOverride : ISerializedItem
    {
        public ModOp operation;
        public SerializedComponent value;

        public AbilityModPropertySequenceOverride()
        {
        }

        public AbilityModPropertySequenceOverride(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            operation = (ModOp) stream.ReadInt32();
            value = new SerializedComponent(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(AbilityModPropertySequenceOverride)}>(" +
                   $"{nameof(operation)}: {operation}, " +
                   $"{nameof(value)}: {value}, " +
                   ")";
        }

        public enum ModOp
        {
            Ignore,
            Override,
        }
    }
}
