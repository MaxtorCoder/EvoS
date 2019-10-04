namespace EvoS.Framework.Assets.Serialized
{
    public abstract class SerializedMonoChildBase : ISerializedItem
    {
        public abstract void Deserialize(AssetFile assetFile, StreamReader stream);
    }
}
