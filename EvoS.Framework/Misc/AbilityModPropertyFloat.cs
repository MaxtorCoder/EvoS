using System;
using EvoS.Framework.Assets;

namespace EvoS.Framework.Misc
{
    [Serializable]
    public class AbilityModPropertyFloat
    {
        public float value;
        public ModOp operation;

        public AbilityModPropertyFloat()
        {
        }

        public AbilityModPropertyFloat(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            value = stream.ReadSingle();
            operation = (ModOp) stream.ReadInt32();
        }

        public override string ToString()
        {
            return $"{nameof(AbilityModPropertyFloat)}>(" +
                   $"{nameof(value)}: {value}, " +
                   $"{nameof(operation)}: {operation}, " +
                   ")";
        }

        public float GetModifiedValue(float input)
        {
            switch (operation)
            {
                case ModOp.Add:
                    return input + value;
                case ModOp.Override:
                    return value;
                case ModOp.Multiply:
                    return input * value;
                default:
                    return input;
            }
        }

        public void CopyValuesFrom(AbilityModPropertyFloat other)
        {
            value = other.value;
            operation = other.operation;
        }

        public enum ModOp
        {
            Ignore,
            Add,
            Override,
            Multiply
        }
    }
}
