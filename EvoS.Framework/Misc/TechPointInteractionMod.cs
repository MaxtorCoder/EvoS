using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("TechPointInteractionMod")]
    public class TechPointInteractionMod : ISerializedItem
    {
        public TechPointInteractionType interactionType;
        public AbilityModPropertyInt modAmount;

        public TechPointInteractionMod()
        {
        }

        public TechPointInteractionMod(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            interactionType = (TechPointInteractionType) stream.ReadInt32();
            modAmount = new AbilityModPropertyInt(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(TechPointInteractionMod)}>(" +
                   $"{nameof(interactionType)}: {interactionType}, " +
                   $"{nameof(modAmount)}: {modAmount}, " +
                   ")";
        }
    }
}
