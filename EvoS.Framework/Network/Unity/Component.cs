using System;
using System.Collections.Generic;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;

namespace EvoS.Framework.Network.Unity
{
    public class Component : ISerializedItem
    {
        public GameObject gameObject;
        public Transform transform;

        public Component GetComponent(Type type)
        {
            return gameObject.GetComponent(type);
        }

        public T GetComponent<T>() where T : Component
        {
            return gameObject.GetComponent<T>();
        }
        
        public IEnumerable<T> GetComponents<T>() where T : Component
        {
            return gameObject.GetComponents<T>();
        }

        public virtual void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            throw new NotImplementedException();
        }
    }
}
