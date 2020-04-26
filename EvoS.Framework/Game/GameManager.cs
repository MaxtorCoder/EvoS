using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using EvoS.Framework.Assets;
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
        private readonly Dictionary<uint, GameObject> NetworkGameObjects = new Dictionary<uint, GameObject>();
        private readonly List<GameObject> _gameObjects = new List<GameObject>();
        public AbilityModManager AbilityModManager;
        public BarrierManager BarrierManager;
        public Board Board;
        public BrushCoordinator BrushCoordinator;
        public CollectTheCoins CollectTheCoins;
        public GameEventManager GameEventManager = new GameEventManager();
        public GameFlowData GameFlowData;
        public GameFlow GameFlow;
        public GameplayData GameplayData;
        public GameplayMutators GameplayMutators = new GameplayMutators();
        public InterfaceManager InterfaceManager;
        public MatchLogger MatchLogger;
        public MatchObjectiveKill MatchObjectiveKill;
        public ObjectivePoints ObjectivePoints;
        public ServerActionBuffer ServerActionBuffer;
        public ServerCombatManager ServerCombatManager;
        public ServerEffectManager ServerEffectManager;
        public SharedActionBuffer SharedActionBuffer;
        public SharedEffectBarrierManager SharedEffectBarrierManager;
        public SpawnPointManager SpawnPointManager;
        public SpoilsManager SpoilsManager;
        public TeamSelectData TeamSelectData;
        public TeamStatusDisplay TeamStatusDisplay;
        public TheatricsManager TheatricsManager;
        private bool s_quitting;
        private GameStatus m_gameStatus;
        private LobbyGameplayOverrides m_gameplayOverrides;
        private LobbyGameplayOverrides m_gameplayOverridesForCurrentGame;
        public Dictionary<int, ForbiddenDevKnowledge> ForbiddenDevKnowledge;
        private Dictionary<int, GamePlayer> GamePlayersByPlayerId = new Dictionary<int, GamePlayer>();
        public event Action OnGameAssembling = () => { };
        public event Action OnGameSelecting = () => { };
        public event Action OnGameLoadoutSelecting = () => { };
        public event Action<GameType> OnGameLaunched = delegate {};
        public event Action<GameManager> OnGameLoaded = (GameManager gameManager) => {gameManager.HandleGameLoaded();};
        public event Action OnGameStarted = () => { };
        public event Action<GameResult> OnGameStopped = delegate { };
        public event Action<GameStatus> OnGameStatusChanged = delegate { };
        public LobbyGameInfo GameInfo { get; private set; }
        public LobbyTeamInfo TeamInfo { get; private set; }
        [JsonIgnore] public LobbyGameConfig GameConfig => GameInfo.GameConfig;

        private bool AllPlayersLoaded;
        private readonly object AllPlayersLoadedLock = new object();
        public LobbyGameplayOverrides GameplayOverrides{get{if (m_gameplayOverridesForCurrentGame != null)return m_gameplayOverridesForCurrentGame;return m_gameplayOverrides;}}
        public LobbyMatchmakingQueueInfo QueueInfo { get; private set; }
        public List<LobbyPlayerInfo> TeamPlayerInfo { get; private set; }
        public LobbyGameSummary GameSummary { get; private set; }
        public LobbyGameSummaryOverrides GameSummaryOverrides { get; private set; }
        public bool EnableHiddenGameItems { get; set; }
        public GameStatus GameStatus => m_gameStatus;
        public float GameStatusTime { get; private set; }
        public static bool IsEditorAndNotGame() => false;
        public AssetLoader MapLoader;
        public AssetLoader AssetsLoader;
        public AssetLoader MiscLoader;




        public GameManager()
        {
            var dummyObject = new GameObject("Fake");
            dummyObject.transform = new Transform();
            dummyObject.AddComponent(GameplayMutators);
            RegisterObject(dummyObject);
            AllPlayersLoaded = false;
        }


        /// <summary>
        /// Init this game and loads all the assets needed to receive players
        /// </summary>
        public void LaunchGame()
        {
            SetGameStatus(GameStatus.Launching);
            MapLoader = new AssetLoader();
            MapLoader.LoadAssetBundle("Bundles/scenes/maps.bundle");
            MapLoader.LoadAsset($"archive:/buildplayer-robotfactory_opu_gamemode/buildplayer-{GameConfig.Map.ToLower()}");
            
            MapLoader.ConstructCaches();


            AssetsLoader = new AssetLoader();
            AssetsLoader.LoadAsset("resources.assets");
            AssetsLoader.ConstructCaches();

            MiscLoader = new AssetLoader();
            MiscLoader.LoadAssetBundle("Bundles/scenes/frontend.bundle");
            MiscLoader.LoadAsset("archive:/buildplayer-options_ui/buildplayer-clientenvironmentsingletons");
            MiscLoader.ConstructCaches();

            SpawnObject(MiscLoader, "ApplicationSingletonsNetId", out _);
            SpawnObject(MiscLoader, "GameSceneSingletons", out var gameSceneSingletons);
            TheatricsManager = gameSceneSingletons.GetComponent<TheatricsManager>();
            AbilityModManager = gameSceneSingletons.GetComponent<AbilityModManager>();
            SpawnObject(MiscLoader, "SharedEffectBarrierManager", out SharedEffectBarrierManager);
            SpawnObject(MiscLoader, "SharedActionBuffer", out SharedActionBuffer);
            SharedActionBuffer.Networkm_actionPhase = ActionBufferPhase.Done;

            SpawnScene(MapLoader, 1, out var commonGameLogic);
            InterfaceManager = commonGameLogic.GetComponent<InterfaceManager>();
            GameFlow = commonGameLogic.GetComponent<GameFlow>();
            MatchLogger = commonGameLogic.GetComponent<MatchLogger>();
            ServerCombatManager = commonGameLogic.GetComponent<ServerCombatManager>();
            ServerEffectManager = commonGameLogic.GetComponent<ServerEffectManager>();
            TeamStatusDisplay = commonGameLogic.GetComponent<TeamStatusDisplay>();
            ServerActionBuffer = commonGameLogic.GetComponent<ServerActionBuffer>();
            TeamSelectData = commonGameLogic.GetComponent<TeamSelectData>();
            BarrierManager = commonGameLogic.GetComponent<BarrierManager>();

            SpawnObject<Board, Board>(MapLoader, out Board);
            SpawnScene(MapLoader, 2, out BrushCoordinator);
            SpawnScene(MapLoader, 3, out var sceneGameLogic);

            GameFlowData = sceneGameLogic.GetComponent<GameFlowData>();
            GameplayData = sceneGameLogic.GetComponent<GameplayData>();
            SpoilsManager = sceneGameLogic.GetComponent<SpoilsManager>();
            ObjectivePoints = sceneGameLogic.GetComponent<ObjectivePoints>();
            SpawnPointManager = sceneGameLogic.GetComponent<SpawnPointManager>();
            MatchObjectiveKill = sceneGameLogic.GetComponent<MatchObjectiveKill>();

            PrintAllNetworkGameObjects();

            foreach (LobbyPlayerInfo playerInfo in TeamInfo.TeamPlayerInfo){
                // We use the ReadyState here to indicate wheter a player or bot finished loading the match
                if (playerInfo.IsNPCBot) {
                    playerInfo.ReadyState = ReadyState.Ready; // Bots are marked as ready as they don't have to load anything
                }
                else
                    playerInfo.ReadyState = ReadyState.Unknown; // Unknown means it is loading
            }

            SetGameStatus(GameStatus.Launched);
        }


        private void HandleGameLoaded() {
            Log.Print(LogType.Debug, "All players loaded");

            GameFlowData.gameState = GameState.SpawningPlayers;

            foreach (GameObject netObj in NetworkGameObjects.Values)
            {
                NetworkIdentity networkIdentity = netObj.GetComponent<NetworkIdentity>();
                networkIdentity.UNetUpdate();
            }
            foreach (var playerInfo in TeamPlayerInfo)
            {
                SpawnPlayerCharacter(playerInfo);
                // actors get synclist updates for currentCardIds and modifiedStats
            }

            // check for owning player
            // TODO
            lock (this.GamePlayersByPlayerId) {
                foreach ((int playerID, GamePlayer player) in this.GamePlayersByPlayerId)
                {
                    foreach (ActorData actor in GameFlowData.GetAllActorsForPlayer(playerID))
                    {
                        player.Connection.Send(4, new OwnerMessage
                        {
                            netId = actor.netId,
                            playerControllerId = (short)player.PlayerId
                        });
                    }
                }
            }
            
            

            // The following should be sent after all players have loaded
            foreach (GameObject netObj in NetworkGameObjects.Values)
            {
                ActorTeamSensitiveData actorTeamSensitiveData = netObj.GetComponent<ActorTeamSensitiveData>();
                if (actorTeamSensitiveData == null) continue;

                // Just send the play to an arbitrary location for now
                actorTeamSensitiveData.CallRpcMovement(GameEventManager.EventType.Invalid,
                    new GridPosProp(11, 10, 6), new GridPosProp(11, 10, 5),
                    null, ActorData.MovementType.Teleport, false, false);
            }

            GameFlowData.gameState = GameState.StartingGame;
            UpdateAllNetObjs();

            GameFlowData.gameState = GameState.Deployment;
            UpdateAllNetObjs();

            GameFlowData.gameState = GameState.BothTeams_Decision;
            GameFlowData.Networkm_currentTurn = 1;
            GameFlowData.Networkm_willEnterTimebankMode = true;
            GameFlowData.Networkm_timeRemainingInDecisionOverflow = 5;
            UpdateAllNetObjs();

            GameFlow.CallRpcSetMatchTime(0);
            // kRpcRpcApplyAbilityModById
            foreach (ActorData actor in GameFlowData.GetActors())
            {
                var turnSm = actor.gameObject.GetComponent<ActorTurnSM>();
                turnSm.CallRpcTurnMessage(TurnMessage.TURN_START, 0);
            }
            BarrierManager.CallRpcUpdateBarriers();
            GameFlowData.CallRpcUpdateTimeRemaining(21);

            PrintAllNetworkGameObjects();
        }



        

        

        public void StopGame(GameResult gameResult = GameResult.NoResult)
        {
            SetGameStatus(GameStatus.Stopped, gameResult);
//            GameTime.scale = 1f;
        }

        public static bool IsGameTypeValidForGGPack(GameType gameType)
        {
            if (gameType != GameType.Tutorial && gameType != GameType.Practice)
                return gameType != GameType.Custom;
            return false;
        }

        public GamePlayer GetGamePlayerByPlayerId(int playerId)
        {
            lock (GamePlayersByPlayerId)
            {
                return GamePlayersByPlayerId[playerId];
            }
        }

        public void AddGamePlayer(int playerId, GamePlayer gamePlayer)
        {
            lock (GamePlayersByPlayerId)
            {
                GamePlayersByPlayerId.Add(playerId, gamePlayer);
            }
        }
        

        public void OnPlayerConnect(GameServerConnection connection)
        {
            
            //Log.Print(LogType.Debug, $"Connected player {connection.PlayerInfo.GetHandle()} with playerID {connection.PlayerId} as {connection.PlayerInfo.GetCharacterType().ToString()}");

            GamePlayer gamePlayer;
            GamePlayersByPlayerId.TryGetValue(connection.PlayerId, out gamePlayer);

            if (gamePlayer == null)
            {
                //gamePlayer = new GamePlayer(connection, connection.PlayerId, connection.PlayerInfo.GetAccountId());
                AddGamePlayer(connection.PlayerId, gamePlayer);
            }
            else
            {
                int oldConnectionId = gamePlayer.Connection.connectionId;
                connection.connectionId = oldConnectionId;
            }

            //gamePlayer.PlayerInfo = connection.PlayerInfo;
            //connection.ActiveGame = this;
            
            
            Player gfPlayer = GameFlow.GetPlayerFromConnectionId(connection.connectionId);

            gfPlayer.m_id = (byte) connection.PlayerId;
            gfPlayer.m_valid = true;
            //gfPlayer.m_accountId = connection.PlayerInfo.GetAccountId();
            gfPlayer.m_connectionId = connection.connectionId;

            GameFlow.playerDetails[gfPlayer] = new PlayerDetails(PlayerGameAccountType.Human)
            {
                //m_team =  this.TeamInfo.GetPlayer(connection.PlayerInfo.GetAccountId()).TeamId,
                //m_handle = this.TeamInfo.GetPlayer(connection.PlayerInfo.GetAccountId()).Handle,
                m_accountId = gfPlayer.m_accountId,
                //m_lobbyPlayerInfoId = this.TeamInfo.TeamPlayerInfo.FindIndex((LobbyPlayerInfo p)=>{ return p.AccountId == connection.PlayerInfo.GetAccountId(); })
            };

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
                    new CRCMessageEntry("SinglePlayerManager", 0)
                }
            });

            connection.RegisterHandler<AssetsLoadingProgress>((short)MyMsgType.ClientAssetsLoadingProgressUpdate, GetGamePlayerByPlayerId(connection.PlayerId), OnAssetLoadingProgress);
            connection.RegisterHandler<AssetsLoadedNotification>((short)MyMsgType.AssetsLoadedNotification, GetGamePlayerByPlayerId(connection.PlayerId), OnAssetsLoadedNotification);

            /*lock (AllPlayersLoadedLock)
            {
                if (AllPlayersLoaded) {
                    foreach (LobbyPlayerInfo player in TeamInfo.TeamPlayerInfo)
                    {
                        if (player.ReadyState == ReadyState.Ready)
                        {
                            connection.Send((short)MyMsgType.AssetsLoadedNotification, new AssetsLoadedNotification { AccountId = player.AccountId, PlayerId = player.PlayerId });
                        }
                    }
                }
                
            }
            //*/
        }

        private void OnAssetLoadingProgress(GamePlayer player, AssetsLoadingProgress msg)
        {
            
            // Send a loading progress message to all players
            foreach(int playerId in GamePlayersByPlayerId.Keys)
            {
                
                GamePlayer humanplayer = GetGamePlayerByPlayerId(playerId);
                if (player.PlayerId != humanplayer.PlayerId)
                    //Log.Print(LogType.Debug, $"sending loading notification to {humanplayer.PlayerInfo.GetHandle()} from {player.PlayerInfo.GetHandle()}");
                    humanplayer.Connection.Send((short)MyMsgType.ServerAssetsLoadingProgressUpdate, msg);
            }
        }

        private void OnAssetsLoadedNotification(GamePlayer player, AssetsLoadedNotification msg)
        {
            player.Connection.Send(56, new ReconnectReplayStatus {WithinReconnectReplay = true});
            player.Connection.Send(54, new SpawningObjectsNotification
            {
                PlayerId = player.PlayerId,
                SpawnableObjectCount = NetworkGameObjects.Count
            });
            player.Connection.Send(12, new ObjectSpawnFinishedMessage {state = 0});

            foreach (GameObject netObj in NetworkGameObjects.Values)
            {
                NetworkIdentity networkIdentityComponent = netObj.GetComponent<NetworkIdentity>();
                networkIdentityComponent.AddObserver(player.Connection);
            }

            player.Connection.Send(56, new ReconnectReplayStatus {WithinReconnectReplay = false});
            player.Connection.Send(12, new ObjectSpawnFinishedMessage {state = 1});
            player.IsLoading = false;
            

            lock (AllPlayersLoadedLock) {

                bool allLoaded = true;
                foreach (LobbyPlayerInfo playerInfo in TeamInfo.TeamPlayerInfo)
                {
                    if (player.PlayerId == playerInfo.PlayerId)
                    {
                        playerInfo.ReadyState = ReadyState.Ready;
                    }
                    
                    if (playerInfo.ReadyState != ReadyState.Ready)
                    {
                        allLoaded = false;
                        Log.Print(LogType.Network, $"{playerInfo.Handle} not loaded");
                    }
                }

                if (allLoaded)
                {
                    Log.Print(LogType.Debug, "All Player loaded successfully");
                    SetGameStatus(GameStatus.Loaded);
                }
                else {
                    Log.Print(LogType.Debug, "Not All Player loaded");
                }
            }
        }


        private void SpawnPlayerCharacter(LobbyPlayerInfo playerInfo)
        {
            //# 
            Log.Print(LogType.Error, "SpawnPlayerCharacter " + playerInfo.CharacterInfo.CharacterType.ToString());

            GamePlayer gamePlayer = GetGamePlayerByPlayerId(playerInfo.PlayerId);

            

            SpawnObject<ActorTeamSensitiveData>(MiscLoader, "ActorTeamSensitiveData_Friendly", out ActorTeamSensitiveData characterFriendly);
            SpawnObject(AssetsLoader, playerInfo.CharacterInfo.CharacterType.ToString(), out GameObject character);

            ActorData characterActorData = character.GetComponent<ActorData>();
            characterActorData.SetClientFriendlyTeamSensitiveData(characterFriendly);
            characterActorData.ServerLastKnownPosSquare = Board.GetBoardSquare(5, 5);
            characterActorData.InitialMoveStartSquare = Board.GetBoardSquare(5, 5);
            characterActorData.UpdateDisplayName(playerInfo.Handle);
            characterActorData.PlayerIndex = playerInfo.PlayerId;
            //characterActorData.ActorIndex = playerInfo.PlayerId;

            PlayerData characterPlayerData = character.GetComponent<PlayerData>();
            
            characterPlayerData.m_player = GameFlow.GetPlayerFromConnectionId(gamePlayer.Connection.connectionId); // TODO hardcoded connection id
            characterPlayerData.PlayerIndex = playerInfo.PlayerId;
            
            
            
            characterFriendly.SetActorIndex(characterActorData.ActorIndex);



            characterActorData.SetTeam(playerInfo.TeamId);

            GameFlowData.AddPlayer(character);

            var netChar = character.GetComponent<NetworkIdentity>();
            var netAtsd = characterFriendly.GetComponent<NetworkIdentity>();
            lock (GamePlayersByPlayerId) {
                foreach (GamePlayer player in GamePlayersByPlayerId.Values)
                {
                    netChar.AddObserver(player.Connection);
                    netAtsd.AddObserver(player.Connection);
                }
            }
        }


        public void UpdateAllNetObjs(){
            foreach (GameObject netObj in NetworkGameObjects.Values){
                var networkIdentityComponent = netObj.GetComponent<NetworkIdentity>();
                networkIdentityComponent.UNetUpdate();
            }
        }


        public void PrintAllNetworkGameObjects(){
            foreach ((uint netId, GameObject obj) in NetworkGameObjects){
                Console.WriteLine($"--!!-- {netId}: {obj}");
                IReadOnlyCollection<Component>objs = obj.GetComponents();
                foreach (Component c in objs) {
                    Console.WriteLine("          "+c.name);
                }

            }
        }

        

        public void RegisterObject(GameObject gameObj){
            if (gameObj.GameManager == this)
                return;
            if (gameObj.GameManager != null)
                throw new InvalidOperationException($"Object registered with another GameManager! {gameObj}");

            gameObj.GameManager = this;
            _gameObjects.Add(gameObj);

            foreach (var component in gameObj.GetComponents<MonoBehaviour>().ToList()){
                component.Awake();
            }

            // recursively register children and parent
            foreach (var child in gameObj.transform.children){
                RegisterObject(child.gameObject);
            }

            if (gameObj.transform.father?.gameObject != null)
                RegisterObject(gameObj.transform.father.gameObject);

            var netIdent = gameObj.GetComponent<NetworkIdentity>();
            if (netIdent != null) {
                netIdent.OnStartServer();
                NetworkGameObjects.Add(netIdent.netId.Value, gameObj);
            }
        }

        public void SpawnObject<T, TR>(AssetLoader loader, out TR component) where T : MonoBehaviour where TR : Component{
            SpawnObject<T>(loader, out var obj, false);
            component = obj.GetComponent<TR>();
            RegisterObject(obj);
        }

        public void SpawnObject<T>(AssetLoader loader, out GameObject obj, bool register = true) where T : MonoBehaviour{
            loader.ClearCache();
            obj = loader.GetObjectByComponent<T>().Instantiate();
            if (register) RegisterObject(obj);
        }

        public void SpawnObject<T>(AssetLoader loader, string name, out T component) where T : Component{
            SpawnObject(loader, name, out var obj, false);
            component = obj.GetComponent<T>();
            RegisterObject(obj);
        }

        public void SpawnObject(AssetLoader loader, string name, out GameObject obj, bool register = true){
            loader.ClearCache();
            obj = loader.NetObjsByName[name].Instantiate();
            if (register) RegisterObject(obj);
        }

        public void SpawnScene(AssetLoader loader, uint sceneId, out GameObject scene, bool register = true){
            foreach (var o in NetworkGameObjects.Where(o => o.Value.GetComponent<NetworkIdentity>().sceneId.Value == sceneId)){
                scene = o.Value;
                return;
            }

            loader.ClearCache();
            scene = loader.NetworkScenes[sceneId].Instantiate();
            if (register) RegisterObject(scene);
        }

        public void SpawnScene<T>(AssetLoader loader, uint sceneId, out T component) where T : Component{
            foreach ( (uint netId, GameObject netObj) in NetworkGameObjects.Where(o => o.Value.GetComponent<NetworkIdentity>().sceneId.Value == sceneId)){
                component = netObj.GetComponent<T>();
                return;
            }

            SpawnScene(loader, sceneId, out var scene, false);
            component = scene.GetComponent<T>();
            RegisterObject(scene);
        }

        public void SetGameInfo(LobbyGameInfo gameInfo){GameInfo = gameInfo;}
        public void SetQueueInfo(LobbyMatchmakingQueueInfo queueInfo){QueueInfo = queueInfo;}
        public void SetTeamInfo(LobbyTeamInfo teamInfo){

            TeamInfo = teamInfo;SetTeamPlayerInfo(teamInfo.TeamPlayerInfo);
        }
        public void SetTeamPlayerInfo(List<LobbyPlayerInfo> teamPlayerInfo){TeamPlayerInfo = teamPlayerInfo;}
        public void SetGameSummary(LobbyGameSummary gameSummary){GameSummary = gameSummary;}
        public void SetGameSummaryOverrides(LobbyGameSummaryOverrides gameSummaryOverrides){GameSummaryOverrides = gameSummaryOverrides;}

        public void SetGameStatus(GameStatus gameStatus, GameResult gameResult = GameResult.NoResult, bool notify = true)
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
                    OnGameLoaded(this);
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


    }
}
