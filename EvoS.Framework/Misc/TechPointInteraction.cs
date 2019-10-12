using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    public struct TechPointInteraction : ISerializedItem
    {
        public TechPointInteractionType Type;
        public int Amount;
        
        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            Type = (TechPointInteractionType) stream.ReadInt32();
            Amount = stream.ReadInt32();
        }

        public override string ToString()
        {
            return $"{nameof(TechPointInteraction)}(" +
                   $"{nameof(Type)}: {Type}, " +
                   $"{nameof(Amount)}: {Amount}" +
                   $")";
        }
    }
}
