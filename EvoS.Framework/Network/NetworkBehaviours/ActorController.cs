using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("ActorController")]
    public class ActorController : NetworkBehaviour

    {
        public ActorController()
        {
        }

        public ActorController(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override bool OnSerialize(NetworkWriter writer, bool forceAll)
        {
            bool flag = false;
            return flag;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
        }

        public override string ToString()
        {
            return $"{nameof(ActorController)}>(" +
                   ")";
        }

        public enum PingType
        {
            Default,
            Assist,
            Defend,
            Enemy,
            Move,
        }
    }
}
