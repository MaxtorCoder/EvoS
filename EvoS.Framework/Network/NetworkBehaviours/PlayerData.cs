using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("PlayerData")]
    public class PlayerData : NetworkBehaviour
    {
        public SerializedComponent ActorData;

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
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
