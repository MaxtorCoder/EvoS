using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("PlayerData")]
    public class PlayerData : ISerializedItem
    {
        public SerializedComponent ActorData;

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            ActorData = new SerializedComponent(assetFile, stream); // class ActorData
        }

        public override string ToString()
        {
            return $"{nameof(PlayerData)}>(" +
                   $"{nameof(ActorData)}: {ActorData}, " +
                   ")";
        }
    }
}
