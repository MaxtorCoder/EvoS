using System;
using System.Collections.Generic;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Game;
using EvoS.Framework.Misc;
using EvoS.Framework.Network.NetworkBehaviours;
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

        public GameManager GameManager => gameObject.GameManager;
        public Board Board => gameObject.GameManager.Board;
        public BrushCoordinator BrushCoordinator => gameObject.GameManager.BrushCoordinator;
        public NetworkServer NetworkServer => gameObject.GameManager.NetworkServer;
        public GameFlow GameFlow => gameObject.GameManager.GameFlow;
        public GameFlowData GameFlowData => gameObject.GameManager.GameFlowData;
        public GameEventManager GameEventManager => gameObject.GameManager.GameEventManager;
        public MatchLogger MatchLogger => gameObject.GameManager.MatchLogger;
        public CollectTheCoins CollectTheCoins => gameObject.GameManager.CollectTheCoins;
        public GameplayData GameplayData => gameObject.GameManager.GameplayData;
        public GameplayMutators GameplayMutators => gameObject.GameManager.GameplayMutators;

        public string name
        {
            get => gameObject.Name;
            set => gameObject.Name = value;
        }
    }
}
