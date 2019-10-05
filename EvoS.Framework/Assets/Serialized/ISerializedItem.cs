namespace EvoS.Framework.Assets.Serialized
{
    public interface ISerializedItem
    {
        void DeserializeAsset(AssetFile assetFile, StreamReader stream);
    }
}
