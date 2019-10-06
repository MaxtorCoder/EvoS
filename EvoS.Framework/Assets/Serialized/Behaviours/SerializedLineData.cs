namespace EvoS.Framework.Assets.Serialized.Behaviours
{
    [SerializedMonoBehaviour("LineData")]
    public class SerializedLineData : ISerializedItem
    {
        public float LastAllyMovementChange { get; set; }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
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
