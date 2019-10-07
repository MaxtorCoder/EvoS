using System;
using EvoS.Framework.Assets;

namespace EvoS.Framework.Misc
{
    [Serializable]
    public class AbilityModPropertyBool
    {
        public bool value;
        public ModOp operation;

        public AbilityModPropertyBool()
        {
        }

        public AbilityModPropertyBool(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            value = stream.ReadBoolean();
            stream.AlignTo();
            operation = (ModOp) stream.ReadInt32();
        }

        public override string ToString()
        {
            return $"{nameof(AbilityModPropertyBool)}>(" +
                   $"{nameof(value)}: {value}, " +
                   $"{nameof(operation)}: {operation}, " +
                   ")";
        }

        public bool GetModifiedValue(bool input)
        {
            return operation == ModOp.Override ? value : input;
        }

        public void CopyValuesFrom(AbilityModPropertyBool other)
        {
            value = other.value;
            operation = other.operation;
        }

        public enum ModOp
        {
            Ignore,
            Override
        }
    }
}
