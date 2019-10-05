using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Assets.Serialized.Behaviours
{
    [SerializedMonoBehaviour("LineData")]
    public class SerializedLineData : SerializedMonoChildBase
    {
        public float LastAllyMovementChange { get; set; }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            stream.AlignTo();
            LastAllyMovementChange = stream.ReadSingle();
        }


        public override string ToString()
        {
            return $"{nameof(SerializedLineData)}(" +
                   $"{nameof(LastAllyMovementChange)}: {LastAllyMovementChange}" +
                   ")";
        }
    }
}
