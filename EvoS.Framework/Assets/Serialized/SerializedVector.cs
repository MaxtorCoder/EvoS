using System;
using System.Collections.Generic;

namespace EvoS.Framework.Assets.Serialized
{
    public class SerializedVector<T> : List<T>, ISerializedItem where T : ISerializedItem
    {
        public void Deserialize(AssetFile assetFile, StreamReader stream)
        {
            Clear();

            var length = stream.ReadInt32();
            for (var i = 0; i < length; i++)
            {
                var item = (T) Activator.CreateInstance(typeof(T));
                item.Deserialize(assetFile, stream);
                Add(item);
            }
        }
    }
}
