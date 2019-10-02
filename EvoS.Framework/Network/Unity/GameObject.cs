using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EvoS.Framework.Network.Unity
{
    public class GameObject
    {
        public string Name { get; private set; }
        private readonly List<Component> _components = new List<Component>();
        private readonly Dictionary<Type, Component> _componentsByType = new Dictionary<Type, Component>();

        public GameObject() : this(null)
        {
        }

        public GameObject(string name, params Type[] components)
        {
            Name = name;
            foreach (Type componentType in components)
            {
                AddComponent(componentType);
            }
        }

        public IEnumerable<T> GetComponents<T>() where T : Component
        {
            foreach (var component in _components)
            {
                if (component.GetType().IsSubclassOf(typeof(T)))
                {
                    yield return (T) component;
                }
            }
        }

        public ReadOnlyCollection<Component> GetComponents()
        {
            return _components.AsReadOnly();
        }

        private void AddComponent(Type type)
        {
            var component = (Component) Activator.CreateInstance(type);
            component.gameObject = this;

            _components.Add(component);
            _componentsByType.Add(type, component);
        }

        public Component GetComponent(Type type)
        {
            return _componentsByType[type];
        }

        public T GetComponent<T>() where T : Component
        {
            return (T) _componentsByType[typeof(T)];
        }
    }
}
