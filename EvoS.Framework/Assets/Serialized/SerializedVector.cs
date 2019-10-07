using System;
using System.Collections.Generic;

namespace EvoS.Framework.Assets.Serialized
{
    public class SerializedVector<T> : List<T>, ISerializedItem
    {
        public SerializedVector()
        {
            
        }
        
        public SerializedVector(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            Clear();

            var length = stream.ReadInt32();
            if (typeof(T) == typeof(string))
            {
                for (var i = 0; i < length; i++)
                {
                    Add((T) (object) stream.ReadString32());
                }
            }
            else 
            if (typeof(T) == typeof(int))
            {
                for (var i = 0; i < length; i++)
                {
                    Add((T) (object) stream.ReadInt32());
                }
            }
            else if (typeof(T).IsEnum)
            {
                for (var i = 0; i < length; i++)
                {
                    Add((T) (object) stream.ReadInt32());
                }
            }
            else if (typeof(SerializedComponent).IsAssignableFrom(typeof(T)) || typeof(T).IsValueType)
            {
                for (var i = 0; i < length; i++)
                {
                    var item = (ISerializedItem) Activator.CreateInstance(typeof(T));
                    item.DeserializeAsset(assetFile, stream);
                    Add((T) item);
                }
            }
            else if (typeof(ISerializedItem).IsAssignableFrom(typeof(T)))
            {
                for (var i = 0; i < length; i++)
                {
                    var ptr = (SerializedComponent) Activator.CreateInstance(typeof(SerializedComponent));
                    ptr.DeserializeAsset(assetFile, stream);
                    Add((T) ptr.LoadValue());
                }
            }
            else
            {
                throw new NotImplementedException($"Unhandled vector child type {typeof(T)}");
            }
        }

        public override string ToString()
        {
            return $"Vector<{typeof(T).Name}>(" +
                   $"{Count} entries" +
                   ")";
        }
    }
}
