using System;
using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Logging;
using EvoS.Framework.Misc;
using EvoS.Framework.Network.Static;
using EvoS.GameServer.Network.Messages.Unity;
using EvoS.GameServer.Network.Unity;

namespace EvoS.GameServer.Network
{
    public class GameManager
    {
        public NetworkServer NetworkServer = new NetworkServer();
        private bool s_quitting;
        private GameStatus m_gameStatus;
        private LobbyGameplayOverrides m_gameplayOverrides;
        private LobbyGameplayOverrides m_gameplayOverridesForCurrentGame;
        public Dictionary<int, ForbiddenDevKnowledge> ForbiddenDevKnowledge;
        private Dictionary<short, GamePlayer> _players = new Dictionary<short, GamePlayer>();

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

        public LobbyGameConfig GameConfig => GameInfo.GameConfig;

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

        public GameStatus GameStatus
        {
            get { return m_gameStatus; }
        }

        public float GameStatusTime { get; private set; }

        internal static bool IsEditorAndNotGame()
        {
            return false;
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

        internal void SetGameStatus(GameStatus gameStatus, GameResult gameResult = GameResult.NoResult,
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
                Log.Print(LogType.GameServer,
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

        public void AddPlayer(ClientConnection connection, AddPlayerMessage msg)
        {
            
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
    }
}
