using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EvoS.Framework.Game;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.Unity
{
    public class GameObject
    {
        public string Name { get; private set; }
        private readonly List<Component> _components = new List<Component>();
        [JsonIgnore]
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

            foreach (var component in GetComponents<MonoBehaviour>().ToList())
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

        private Component AddComponent(Type type)
        {
            return AddComponent((Component) Activator.CreateInstance(type));
        }

        public T AddComponent<T>() where T : Component
        {
            return (T) AddComponent(typeof(T));
        }

        public Component AddComponent(Component component)
        {
            component.gameObject = this;

            _components.Add(component);

            if (GameManager != null && component is MonoBehaviour behaviour)
            {
                behaviour.Awake();
            }

            return component;
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
