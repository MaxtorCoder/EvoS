using System;
using System.Collections.Generic;
using System.Security;

namespace EvoS.GameServer.Network.Unity
{
    public class Component
    {
        public GameObject gameObject;
        public Transform transform = new Transform();

        public Component GetComponent(Type type)
        {
            return gameObject.GetComponent(type);
        }

        public T GetComponent<T>() where T : Component
        {
            return gameObject.GetComponent<T>();
        }
        
        protected IEnumerable<T> GetComponents<T>() where T : Component
        {
            return gameObject.GetComponents<T>();
        }
    }
}
