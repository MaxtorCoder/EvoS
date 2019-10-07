using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    public class PassiveEventData : ISerializedItem
    {
        public PassiveEventType EventType;

        public SerializedVector<SerializedMonoBehaviour> CausalAbilities;

//        public PassiveEventResponse ResponseOnSelf;
//        public PassiveEventResponse ResponseOnOther;
        public SerializedComponent ResponseOnSelf;
        public SerializedComponent ResponseOnOther;

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            EventType = (PassiveEventType) (object) stream.ReadInt32();
            CausalAbilities = new SerializedVector<SerializedMonoBehaviour>();
//            CausalAbilities.DeserializeAsset(assetFile, stream);
            ResponseOnSelf = new SerializedComponent(assetFile, stream);
            ResponseOnOther = new SerializedComponent(assetFile, stream);
        }
    }
}
