using System;
using System.IO;

namespace EvoS.Framework.Assets.Serialized
{
    public class SerializedComponent : ISerializedItem
    {
        public int FileId { get; set; }
        public long PathId { get; set; }
        private WeakReference<AssetFile> _assetFile;
        private WeakReference<ISerializedItem> _cachedValue = new WeakReference<ISerializedItem>(null);

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            _assetFile = new WeakReference<AssetFile>(assetFile);

            FileId = stream.ReadInt32();
            PathId = stream.ReadInt64();
        }

        public ISerializedItem LoadValue()
        {
            if (_cachedValue.TryGetTarget(out var value))
            {
                return value;
            }

            if (!_assetFile.TryGetTarget(out var assetFile))
            {
                return null;
            }

            value = assetFile.ReadObject(PathId, FileId);
            _cachedValue.SetTarget(value);
            return value;
        }
    }
}
