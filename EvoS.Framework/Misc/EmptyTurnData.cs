using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    public class EmptyTurnData : ISerializedItem
    {
        public TurnEmptinessLevel TriggerLevel;
        public PassiveEventResponse ResponseOnSelf;
        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            TriggerLevel = (TurnEmptinessLevel) stream.ReadInt32();
            ResponseOnSelf = (PassiveEventResponse) new SerializedComponent(assetFile, stream).LoadValue();
        }
    }
}
