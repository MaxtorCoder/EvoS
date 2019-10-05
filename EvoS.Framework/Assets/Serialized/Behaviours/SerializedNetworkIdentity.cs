using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Assets.Serialized.Behaviours
{
    [SerializedMonoBehaviour("NetworkIdentity")]
    public class SerializedNetworkIdentity : SerializedMonoChildBase
    {
        public NetworkSceneId SceneId { get; set; }
        public Hash128 AssetId { get; set; }
        public bool ServerOnly { get; set; }
        public bool LocalPlayerAuthority { get; set; }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            stream.AlignTo();
            SceneId = new NetworkSceneId(stream.ReadUInt32());

            AssetId = new Hash128();
            AssetId.DeserializeAsset(assetFile, stream);
            ServerOnly = stream.ReadBoolean();
            stream.AlignTo();
            LocalPlayerAuthority = stream.ReadBoolean();
            stream.AlignTo();
        }


        public override string ToString()
        {
            return $"{nameof(SerializedNetworkIdentity)}(" +
                   $"{nameof(SceneId)}: {SceneId}, " +
                   $"{nameof(AssetId)}: {AssetId}" +
                   ")";
        }
    }
}
