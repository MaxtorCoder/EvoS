using System;
using System.Collections.Generic;

namespace EvoS.Framework.Assets.Serialized
{
    public class SerializedVector<T> : List<T>, ISerializedItem where T : ISerializedItem
    {
        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            Clear();

            var length = stream.ReadInt32();
            for (var i = 0; i < length; i++)
            {
                var item = (T) Activator.CreateInstance(typeof(T));
                item.DeserializeAsset(assetFile, stream);
                Add(item);
            }
        }
    }
}
