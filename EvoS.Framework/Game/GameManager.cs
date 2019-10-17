using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Logging;
using EvoS.Framework.Misc;
using EvoS.Framework.Network;
using EvoS.Framework.Network.Game;
using EvoS.Framework.Network.Game.Messages;
using EvoS.Framework.Network.NetworkBehaviours;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.Unity;
using EvoS.Framework.Network.Unity.Messages;
using Newtonsoft.Json;

namespace EvoS.Framework.Game
{
    public class GameManager
    {
        public readonly NetworkServer NetworkServer = new NetworkServer();
        private readonly Dictionary<uint, GameObject> _netObjects = new Dictionary<uint, GameObject>();
        private readonly List<GameObject> _gameObjects = new List<GameObject>();
        public Board Board;
        public GameFlow GameFlow;
        public GameFlowData GameFlowData;
        public GameEventManager GameEventManager = new GameEventManager();
        public MatchLogger MatchLogger;
        public GameplayData GameplayData;
        public BrushCoordinator BrushCoordinator;
        public CollectTheCoins CollectTheCoins;
        public GameplayMutators GameplayMutators = new GameplayMutators();
        private bool s_quitting;
        private GameStatus m_gameStatus;
        private LobbyGameplayOverrides m_gameplayOverrides;
        private LobbyGameplayOverrides m_gameplayOverridesForCurrentGame;
        public Dictionary<int, ForbiddenDevKnowledge> ForbiddenDevKnowledge;
        private Dictionary<int, GamePlayer> _players = new Dictionary<int, GamePlayer>();

        public event Action OnGameAssembling = () => { };

        public event Action OnGameSelecting = () => { };

        public event Action OnGameLoadoutSelecting = () => { };

        public event Action<GameType> OnGameLaunched = delegate { };

        public event Action OnGameLoaded = () => { };

        public event Action OnGameStarted = () => { };

        public event Action<GameResult> OnGameStopped = delegate { };

        public event Action<GameStatus> OnGameStatusChanged = delegate { };

        public LobbyGameInfo GameInfo { get; private set; }

        public LobbyTeamInfo TeamInfo { get; private set; }

        [JsonIgnore] public LobbyGameConfig GameConfig => GameInfo.GameConfig;

        public LobbyGameplayOverrides GameplayOverrides
        {
            get
            {
                if (m_gameplayOverridesForCurrentGame != null)
                    return m_gameplayOverridesForCurrentGame;
                return m_gameplayOverrides;
            }
        }

        public LobbyMatchmakingQueueInfo QueueInfo { get; private set; }

        public List<LobbyPlayerInfo> TeamPlayerInfo { get; private set; }

        public LobbyPlayerInfo PlayerInfo { get; private set; }

        public LobbyGameSummary GameSummary { get; private set; }

        public LobbyGameSummaryOverrides GameSummaryOverrides { get; private set; }

        public bool EnableHiddenGameItems { get; set; }

        public GameStatus GameStatus => m_gameStatus;

        public float GameStatusTime { get; private set; }

        public static bool IsEditorAndNotGame() => false;

        public GameManager()
        {
            var dummyObject = new GameObject("Fake");
            dummyObject.transform = new Transform();
            dummyObject.AddComponent(GameplayMutators);
            RegisterObject(dummyObject);
        }

        public void Reset()
        {
            GameInfo = new LobbyGameInfo();
            TeamInfo = new LobbyTeamInfo();
            m_gameplayOverrides = new LobbyGameplayOverrides();
            m_gameStatus = GameStatus.Stopped;
            QueueInfo = null;
            ForbiddenDevKnowledge = null;
//            if (!((UnityEngine.Object) GameWideData.Get() != (UnityEngine.Object) null))
//                return;
//            this.GameplayOverrides.SetBaseCharacterConfigs(GameWideData.Get());
        }

        public void SetGameStatus(GameStatus gameStatus, GameResult gameResult = GameResult.NoResult,
            bool notify = true)
        {
            if (gameStatus == m_gameStatus)
                return;
            m_gameStatus = gameStatus;
//            this.GameStatusTime = Time.unscaledTime; // TODO
            if (s_quitting || !notify)
                return;
            if (!GameInfo.GameServerProcessCode.IsNullOrEmpty() && GameInfo.GameConfig != null)
            {
                Log.Print(LogType.Game,
                    gameResult == GameResult.NoResult
                        ? $"Game {GameInfo.Name} is {gameStatus.ToString().ToLower()}"
                        : $"Game {GameInfo.Name} is {gameStatus.ToString().ToLower()} with result {gameResult.ToString()}");
            }

            switch (gameStatus)
            {
                case GameStatus.Assembling:
                    OnGameAssembling();
                    break;
                case GameStatus.FreelancerSelecting:
                    OnGameSelecting();
                    break;
                case GameStatus.LoadoutSelecting:
                    OnGameLoadoutSelecting();
                    break;
                case GameStatus.Launched:
                    OnGameLaunched(GameInfo.GameConfig.GameType);
                    break;
                case GameStatus.Loaded:
                    OnGameLoaded();
                    break;
                case GameStatus.Started:
                    OnGameStarted();
                    break;
                case GameStatus.Stopped:
                    OnGameStopped(gameResult);
                    break;
            }

            OnGameStatusChanged(gameStatus);
        }

        public void SetGameInfo(LobbyGameInfo gameInfo)
        {
            GameInfo = gameInfo;
        }

        public void SetQueueInfo(LobbyMatchmakingQueueInfo queueInfo)
        {
            QueueInfo = queueInfo;
        }

        public void SetTeamInfo(LobbyTeamInfo teamInfo)
        {
            TeamInfo = teamInfo;
        }

        public void SetPlayerInfo(LobbyPlayerInfo playerInfo)
        {
            PlayerInfo = playerInfo;
        }

        public void SetTeamPlayerInfo(List<LobbyPlayerInfo> teamPlayerInfo)
        {
            TeamPlayerInfo = teamPlayerInfo;
        }

        public void SetGameSummary(LobbyGameSummary gameSummary)
        {
            GameSummary = gameSummary;
        }

        public void SetGameSummaryOverrides(LobbyGameSummaryOverrides gameSummaryOverrides)
        {
            GameSummaryOverrides = gameSummaryOverrides;
        }

        public void StopGame(GameResult gameResult = GameResult.NoResult)
        {
            SetGameStatus(GameStatus.Stopped, gameResult);
//            GameTime.scale = 1f;
        }

        public bool IsGameLoading()
        {
            bool flag = false;
            if (GameInfo != null && GameInfo.GameConfig != null &&
                GameInfo.GameStatus != GameStatus.Stopped)
            {
                if (GameInfo.GameConfig.GameType != GameType.Custom)
                {
                    if (GameInfo.GameStatus >= GameStatus.Assembling)
                        flag = true;
                }
                else if (GameInfo.GameStatus.IsPostLaunchStatus())
                    flag = true;
            }

            return flag;
        }

        public static bool IsGameTypeValidForGGPack(GameType gameType)
        {
            if (gameType != GameType.Tutorial && gameType != GameType.Practice)
                return gameType != GameType.Custom;
            return false;
        }

        public void AddPlayer(ClientConnection connection, LoginRequest loginReq, AddPlayerMessage msg)
        {
            _players.Add(loginReq.PlayerId, new GamePlayer(connection, loginReq, msg));

            // This isn't actually correct, but the client logs a warning with what it expected and continues
            connection.Send(14, new CRCMessage
            {
                scripts = new[]
                {
                    new CRCMessageEntry("ActorData", 0),
                    new CRCMessageEntry("BrushCoordinator", 0),
                    new CRCMessageEntry("ActorController", 0),
                    new CRCMessageEntry("AbilityData", 0),
                    new CRCMessageEntry("ActorStats", 0),
                    new CRCMessageEntry("ActorStatus", 0),
                    new CRCMessageEntry("ActorBehavior", 0),
                    new CRCMessageEntry("PlayerData", 0),
                    new CRCMessageEntry("PowerUp", 0),
                    new CRCMessageEntry("GameFlow", 0),
                    new CRCMessageEntry("TeamStatusDisplay", 0),
                    new CRCMessageEntry("BarrierManager", 0),
                    new CRCMessageEntry("GameFlowData", 0),
                    new CRCMessageEntry("ObjectivePoints", 0),
                    new CRCMessageEntry("CoinCarnageManager", 0),
                    new CRCMessageEntry("ActorTeamSensitiveData", 0),
                    new CRCMessageEntry("ActorAdditionalVisionProviders", 0),
                    new CRCMessageEntry("ActorCinematicRequests", 0),
                    new CRCMessageEntry("FreelancerStats", 0),
                    new CRCMessageEntry("Manta_SyncComponent", 0),
                    new CRCMessageEntry("Rampart_SyncComponent", 0),
                    new CRCMessageEntry("SinglePlayerManager", 0),
                }
            });

            connection.RegisterHandler<AssetsLoadingProgress>(61, _players[loginReq.PlayerId], OnAssetLoadingProgress);
            connection.RegisterHandler<AssetsLoadedNotification>(53, _players[loginReq.PlayerId],
                OnAssetsLoadedNotification);
        }

        private void OnAssetLoadingProgress(GamePlayer player, AssetsLoadingProgress msg)
        {
            // TODO should send to all
            player.Connection.Send(62, msg);
        }

        private void OnAssetsLoadedNotification(GamePlayer player, AssetsLoadedNotification msg)
        {
            player.Connection.Send(56, new ReconnectReplayStatus {WithinReconnectReplay = true});
            player.Connection.Send(54, new SpawningObjectsNotification
            {
                PlayerId = player.LoginRequest.PlayerId,
                SpawnableObjectCount = 43
            });
            player.Connection.Send(12, new ObjectSpawnFinishedMessage {state = 0});
            player.Connection.Send(10, new ObjectSpawnSceneMessage
            {
                netId = new NetworkInstanceId(5),
                sceneId = new NetworkSceneId(1),
                position = Vector3.Zero,
                payload = Convert.FromBase64String("AAAAAAAAAwAAAAADAAAAAA==")
            });
            player.Connection.Send(10, new ObjectSpawnSceneMessage
            {
                netId = new NetworkInstanceId(6),
                sceneId = new NetworkSceneId(2),
                position = new Vector3(18.7f, 2.0f, 19.4f),
                payload = Convert.FromBase64String(
                    "CwD7//////v/////+//////7//////v/////+//////7//////v/////+//////7//////v/////")
            });
            player.Connection.Send(10, new ObjectSpawnSceneMessage
            {
                netId = new NetworkInstanceId(7),
                sceneId = new NetworkSceneId(3),
                position = Vector3.Zero,
                payload = Convert.FromBase64String(
                    "AAAAAAAAAAAAAIA/AADgQAAAoEEAANBBAAAAAAAAAAAAAAIAAAAAAAAAAAAAAAAAAAA=")
            });
            player.Connection.Send(56, new ReconnectReplayStatus {WithinReconnectReplay = false});
            player.Connection.Send(12, new ObjectSpawnFinishedMessage {state = 1});
        }

//        public class ObserverMessage : MessageBase
//        {
//            public Replay.Message Message;
//
//            public override void Serialize(NetworkWriter writer)
//            {
//                GeneratedNetworkCode._WriteMessage_Replay(writer, this.Message);
//            }
//
//            public override void Deserialize(NetworkReader reader)
//            {
//                this.Message = GeneratedNetworkCode._ReadMessage_Replay(reader);
//            }
//        }


        public void RegisterObject(GameObject gameObj)
        {
            if (gameObj.GameManager == this)
                return;
            if (gameObj.GameManager != null)
                throw new InvalidOperationException($"Object registered with another GameManager! {gameObj}");

            gameObj.GameManager = this;
            _gameObjects.Add(gameObj);

            foreach (var component in gameObj.GetComponents<MonoBehaviour>().ToList())
            {
                component.Awake();
            }

            // recursively register children and parent
            foreach (var child in gameObj.transform.children)
            {
                RegisterObject(child.gameObject);
            }
            if (gameObj.transform.father?.gameObject != null)
                RegisterObject(gameObj.transform.father.gameObject);

            var netIdent = gameObj.GetComponent<NetworkIdentity>();
            if (netIdent != null)
            {
                netIdent.OnStartServer();
                _netObjects.Add(netIdent.netId.Value, gameObj);
            }
        }

        public TR SpawnObject<T, TR>(AssetLoader loader) where T : MonoBehaviour where TR : Component
        {
            return SpawnObject<T>(loader).GetComponent<TR>();
        }

        public GameObject SpawnObject<T>(AssetLoader loader) where T : MonoBehaviour
        {
            loader.ClearCache();
            return loader.GetObjectByComponent<T>().Instantiate(this);
        }

        public T SpawnObject<T>(AssetLoader loader, string name) where T : Component
        {
            return SpawnObject(loader, name).GetComponent<T>();
        }

        public GameObject SpawnObject(AssetLoader loader, string name)
        {
            loader.ClearCache();
            return loader.NetObjsByName[name].Instantiate(this);
        }

        public GameObject SpawnScene(AssetLoader loader, uint sceneId)
        {
            loader.ClearCache();
            return loader.NetworkScenes[sceneId].Instantiate(this);
        }
    }
}
