using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("AbilityModPropertyBlockingRules")]
    public class AbilityModPropertyBlockingRules : ISerializedItem
    {
        public ModOp operation;
        public int value;

        public AbilityModPropertyBlockingRules()
        {
        }

        public AbilityModPropertyBlockingRules(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            operation = (ModOp) stream.ReadInt32();
            value = stream.ReadInt32();
        }

        public override string ToString()
        {
            return $"{nameof(AbilityModPropertyBlockingRules)}>(" +
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
