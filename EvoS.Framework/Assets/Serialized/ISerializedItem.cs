namespace EvoS.Framework.Assets.Serialized
{
    public interface ISerializedItem
    {
        void Deserialize(AssetFile assetFile, StreamReader stream);
    }
}
