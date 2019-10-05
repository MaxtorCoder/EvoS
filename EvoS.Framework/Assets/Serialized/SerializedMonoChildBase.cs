namespace EvoS.Framework.Assets.Serialized
{
    public abstract class SerializedMonoChildBase : ISerializedItem
    {
        public abstract void DeserializeAsset(AssetFile assetFile, StreamReader stream);
    }
}
