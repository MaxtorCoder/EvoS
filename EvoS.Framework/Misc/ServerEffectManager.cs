using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("ServerEffectManager")]
    public class ServerEffectManager : MonoBehaviour
    {
        public ServerEffectManager()
        {
        }

        public ServerEffectManager(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
        }

        public override string ToString()
        {
            return $"{nameof(ServerEffectManager)}(" +
                   ")";
        }
    }
}
