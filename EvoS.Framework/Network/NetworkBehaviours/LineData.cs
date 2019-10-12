using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [SerializedMonoBehaviour("LineData")]
    public class LineData : NetworkBehaviour
    {
        public float LastAllyMovementChange { get; set; }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            stream.AlignTo();
            LastAllyMovementChange = stream.ReadSingle();
        }

        public override string ToString()
        {
            return $"{nameof(LineData)}(" +
                   $"{nameof(LastAllyMovementChange)}: {LastAllyMovementChange}" +
                   ")";
        }
    }
}
