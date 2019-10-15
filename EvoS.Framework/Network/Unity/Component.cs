using System;
using System.Collections.Generic;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.Unity
{
    public class Component : ISerializedItem
    {
        [JsonIgnore]
        public GameObject gameObject;
        public Transform transform;
        public bool enabled = true;

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
