using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Game;

namespace EvoS.Framework.Network.Unity
{
    public class GameObject
    {
        public string Name { get; private set; }
        private readonly List<Component> _components = new List<Component>();
        public GameManager GameManager { get; private set; }

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

        public void Register(GameManager manager)
        {
            if (GameManager != null)
            {
                throw new ApplicationException($"GameObject {this} already registered!");
            }

            GameManager = manager;

            foreach (var component in GetComponents<MonoBehaviour>())
            {
                component.Awake();
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
        }

        public void AddComponent(Component component)
        {
            component.gameObject = this;

            _components.Add(component);
        }

        public Component GetComponent(Type type)
        {
            foreach (var component in _components)
            {
                if (component.GetType().IsAssignableFrom(type))
                {
                    return component;
                }
            }

            return null;
        }

        public T GetComponent<T>() where T : Component
        {
            return (T) GetComponent(typeof(T));
        }
    }
}
