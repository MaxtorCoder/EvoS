using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Assets.Serialized.Behaviours
{
    [SerializedMonoBehaviour("ChatterComponent")]
    public class SerializedChatterComponent : MonoBehaviour
    {
        public SerializedVector<SerializedComponent> Chatters { get; set; }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            stream.AlignTo();
            Chatters = new SerializedVector<SerializedComponent>();
            Chatters.DeserializeAsset(assetFile, stream);
        }


        public override string ToString()
        {
            return $"{nameof(SerializedChatterComponent)}(" +
                   $"{nameof(Chatters)}: {Chatters.Count} entries" +
                   ")";
        }
    }
}
