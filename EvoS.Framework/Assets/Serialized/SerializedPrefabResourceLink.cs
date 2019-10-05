namespace EvoS.Framework.Assets.Serialized
{
    public class SerializedPrefabResourceLink : ISerializedItem
    {
        public string ResourcePath { get; set; }
        public string GUID { get; set; }
        public string DebugPrefabPath { get; set; }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            ResourcePath = stream.ReadString32();
            GUID = stream.ReadString32();
            DebugPrefabPath = stream.ReadString32();
        }

        public override string ToString()
        {
            return $"{nameof(SerializedPrefabResourceLink)}(" +
                   $"{nameof(ResourcePath)}: {ResourcePath}, " +
                   $"{nameof(GUID)}: {GUID}, " +
                   $"{nameof(DebugPrefabPath)}: {DebugPrefabPath}" +
                   ")";
        }
    }
}
