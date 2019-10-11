using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("ActorAdditionalVisionProviders")]
    public class ActorAdditionalVisionProviders : NetworkBehaviour
    {
        public ActorAdditionalVisionProviders()
        {
        }

        public ActorAdditionalVisionProviders(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
        }

        public override string ToString()
        {
            return $"{nameof(ActorAdditionalVisionProviders)}>(" +
                   ")";
        }
    }
}
