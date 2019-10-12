namespace EvoS.Framework.Assets.Serialized
{
    public class SerializedArray<T> : SerializedVector<T>
    {
        public SerializedArray() : base(true)
        {
        }
        
        public SerializedArray(AssetFile assetFile, StreamReader stream) : base(assetFile, stream, true)
        {
        }
    }
}
