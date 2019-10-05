using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    public class PassiveEventResponse : ISerializedItem
    {
        public int HealthBonus;
        public int TechPointsBonus;
        public int PersonalCreditsBonus;
        public int MechanicPointAdjust;
        public SerializedVector<PassiveActionType> Actions;
//        public StandardEffectInfo Effect;
        public SerializedComponent Effect;
        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            HealthBonus = stream.ReadInt32();
            TechPointsBonus = stream.ReadInt32();
            PersonalCreditsBonus = stream.ReadInt32();
            MechanicPointAdjust = stream.ReadInt32();
            Actions = new SerializedVector<PassiveActionType>();
            Actions.DeserializeAsset(assetFile, stream);
            Effect = new SerializedComponent(assetFile, stream);
//            Effect = (StandardEffectInfo) (object) stream.ReadInt32();
        }
    }
}
