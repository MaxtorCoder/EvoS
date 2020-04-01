using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Game;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.NetworkMessages;
using EvoS.Framework.Network.Static;
using EvoS.LobbyServer.LobbyQueue;
using EvoS.LobbyServer.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvoS.LobbyServer
{
    class LobbyQueueManager
    {
        private static LobbyQueueManager Instance = null;

        private Dictionary<GameType, LobbyQueue.LobbyQueue> Queues = new Dictionary<GameType, LobbyQueue.LobbyQueue>();
        
        private static long currentMatchId = 0;

        private LobbyQueueManager() {
            CreatePracticeQueue();
            CreateCoopQueue();
            CreatePvPQueue();
            CreateRankedQueue();
            CreateCustomQueue();
        }
        

        public static LobbyQueueManager GetInstance() {
            if (Instance == null) {
                Instance = new LobbyQueueManager();
            }
            return Instance;
        }

        private LobbyQueue.LobbyQueue FindQueue(GameType gameType)
        {
            try
            {
                return Queues[gameType];
            }
            catch (KeyNotFoundException)
            {
                throw new LobbyQueueExceptions.LobbyQueueNotFoundException();
            }
            
        }

        public static void AddPlayerToQueue(ClientConnection client)
        {
            LobbyQueue.LobbyQueue queue = LobbyQueueManager.GetInstance().FindQueue(client.SelectedGameType);
            queue.AddPlayer(client);
            /*
            if (client.SelectedGameType == GameType.Practice)
            {
                StartPracticeGame(client);
            }
            else
            {
                if (client.SelectedGameType == GameType.Coop && client.SelectedSubTypeMask == GameTypesUtils.SubTypeMasks.SoloVsBots)
                {
                    Log.Print(LogType.Debug, "AI TEAMMATES");
                }
                AddPlayerToQueue(client);

                QueuedPlayers[client.SelectedGameType] += 1;
                Instance.Queue.Add(client);

                MatchmakingQueueAssignmentNotification assignmentNotification = new MatchmakingQueueAssignmentNotification()
                {
                    Reason = "",
                    MatchmakingQueueInfo = new LobbyMatchmakingQueueInfo()
                    {
                        ShowQueueSize = true,
                        QueuedPlayers = QueuedPlayers[client.SelectedGameType],
                        PlayersPerMinute = 1,
                        GameConfig = new LobbyGameConfig(),
                        QueueStatus = QueueStatus.Success,
                        AverageWaitTime = TimeSpan.FromSeconds(100),
                        GameType = client.SelectedGameType
                    }
                };

                client.SendMessage(assignmentNotification).Wait();

                ProcessMatchmaking();
            }
            /*
            */
        }

        public static void RemovePlayerFromQueue(ClientConnection client)
        {
            LobbyQueue.LobbyQueue queue = LobbyQueueManager.GetInstance().FindQueue(client.SelectedGameType);
            queue.RemovePlayer(client);
        }

        private void CreatePracticeQueue()
        {
            LobbyGameConfig gameConfig = new LobbyGameConfig()
            {
                GameType = GameType.Practice,
                IsActive = true,
                GameOptionFlags = GameOptionFlag.EnableTeamAIOutput | GameOptionFlag.NoInputIdleDisconnect,
                Spectators = 0,
                TeamAPlayers = 1,
                TeamABots = 0,
                TeamBPlayers = 0,
                TeamBBots = 2,
                ResolveTimeoutLimit = 160,
                RoomName = "default",
                Map = Maps.VR_Practice,
                SubTypes = new List<GameSubType>
                {
                    new GameSubType
                    {
                        DuplicationRule = FreelancerDuplicationRuleTypes.noneInTeam,
                        GameMapConfigs = new List<GameMapConfig> { new GameMapConfig(Maps.VR_Practice) },
                        InstructionsToDisplay = GameSubType.GameLoadScreenInstructions.Default,
                        LocalizedName = "GenericPractice@SubTypes",
                        PersistedStatBucket = PersistedStatBucket.Deathmatch_Unranked,
                        RewardBucket = GameBalanceVars.GameRewardBucketType.NoRewards,
                        RoleBalancingRule = FreelancerRoleBalancingRuleTypes.none,
                        TeamAPlayers = 1,
                        TeamABots = 0,
                        TeamBPlayers = 0,
                        TeamBBots = 2,
                        Mods = new List<GameSubType.SubTypeMods>
                        {
                            GameSubType.SubTypeMods.AllowPlayingLockedCharacters,
                            GameSubType.SubTypeMods.HumansHaveFirstSlots,
                            GameSubType.SubTypeMods.NotAllowedForGroups
                        },
                        TeamComposition = new TeamCompositionRules
                        {
                            Rules = new Dictionary<TeamCompositionRules.SlotTypes, FreelancerSet>
                            {
                                {
                                    TeamCompositionRules.SlotTypes.A1,
                                    FreelancerSet.AllRoles
                                },
                                {
                                    TeamCompositionRules.SlotTypes.B1,
                                    new FreelancerSet{ Types = new List<CharacterType> {CharacterType.PunchingDummy} }
                                },
                                {
                                    TeamCompositionRules.SlotTypes.B2,
                                    new FreelancerSet{ Types = new List<CharacterType> {CharacterType.PunchingDummy} }
                                }
                            }
                        }
                    }
                }
                
            };
            LobbyQueue.LobbyQueue queue = new LobbyQueue.LobbyQueue(GameType.Practice, gameConfig);
            Queues.Add(GameType.Practice, queue);
        }

        private void CreateCoopQueue()
        {
            LobbyGameConfig gameConfig = new LobbyGameConfig()
            {
                GameType = GameType.Coop,
                IsActive = true,
                GameOptionFlags = GameOptionFlag.EnableTeamAIOutput | GameOptionFlag.ReplaceHumansWithBots,
                Spectators = 0,
                TeamAPlayers = 4,
                TeamABots = 0,
                TeamBPlayers = 4,
                TeamBBots = 0,
                ResolveTimeoutLimit = 160,
                RoomName = "default",
                Map = String.Empty,
                SubTypes = new List<GameSubType>
                {
                    new GameSubType
                    {
                        DuplicationRule = FreelancerDuplicationRuleTypes.noneInTeam,
                        GameMapConfigs = GameMapConfig.GetDeatmatchMaps(),
                        InstructionsToDisplay = GameSubType.GameLoadScreenInstructions.Default,
                        LocalizedName = "GenericPvE@SubTypes",
                        PersistedStatBucket = PersistedStatBucket.Deathmatch_Unranked,
                        RewardBucket = GameBalanceVars.GameRewardBucketType.HumanVsBotsRewards,
                        RoleBalancingRule = FreelancerRoleBalancingRuleTypes.balanceBothTeams,
                        TeamAPlayers = 4,
                        TeamABots = 3,
                        TeamBPlayers = 0,
                        TeamBBots = 4,
                        Mods = new List<GameSubType.SubTypeMods>
                        {
                            GameSubType.SubTypeMods.HumansHaveFirstSlots,
                            GameSubType.SubTypeMods.ShowWithAITeammates
                        },
                        TeamComposition = new TeamCompositionRules
                        {
                            Rules = new Dictionary<TeamCompositionRules.SlotTypes, FreelancerSet>
                            {
                                {
                                    TeamCompositionRules.SlotTypes.TeamA,
                                    new FreelancerSet
                                    {
                                        Roles = new List<CharacterRole> { CharacterRole.Assassin, CharacterRole.Tank, CharacterRole.Support },
                                    }
                                },
                                {
                                    TeamCompositionRules.SlotTypes.TeamB,
                                    new FreelancerSet
                                    {
                                        Roles = new List<CharacterRole> { CharacterRole.Assassin, CharacterRole.Tank, CharacterRole.Support }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            LobbyQueue.LobbyQueue queue = new LobbyQueue.LobbyQueue(GameType.Coop, gameConfig);
            Queues.Add(GameType.Coop, queue);
        }

        private void CreatePvPQueue()
        {
        }

        private void CreateRankedQueue()
        {
        }

        private void CreateCustomQueue()
        {
        }

        /*private static void StartPracticeGame(ClientConnection connection) {
            Log.Print(LogType.Debug, "StartPracticeGame");

            //LobbyGameConfig lobbyGameConfig = CreatePracticeLobbyGameConfig();

            LobbyPlayerInfo playerInfo = connection.CreateLobbyPlayerInfo();
            playerInfo.PlayerId = 1;
            playerInfo.ControllingPlayerId = 1;
            playerInfo.TeamId = Team.TeamA;
            playerInfo.ReadyState = ReadyState.Ready;


            GameAssignmentNotification notification = new GameAssignmentNotification();

            notification.GameResult = GameResult.NoResult;
            notification.Observer = false;
            notification.Reconnection = false;
            notification.GameInfo = CreatePracticeLobbyGameInfo();
            notification.GameplayOverrides = DummyLobbyData.CreateLobbyGameplayOverrides();
            notification.PlayerInfo = playerInfo;
            
            //Log.Print(LogType.Network, $"Responding {JsonConvert.SerializeObject(practice)}");
            _ = connection.SendMessage(notification);

            GameInfoNotification gameInfoNotification = new GameInfoNotification
            {
                GameInfo = CreatePracticeLobbyGameInfo(),
                PlayerInfo = playerInfo,
                TeamInfo = new LobbyTeamInfo
                {
                    TeamPlayerInfo = new List<LobbyPlayerInfo>{ playerInfo }
                }
            };
            gameInfoNotification.GameInfo.GameServerAddress = "ws://127.0.0.1:6061";
            gameInfoNotification.GameInfo.GameStatus = GameStatus.Launched;
            gameInfoNotification.GameInfo.GameServerHost = "Practice Game Host";

            //var practiceGameInfo = DummyLobbyData.CreatePracticeGameInfoNotification(connection);
            //Log.Print(LogType.Network, $"Responding {JsonConvert.SerializeObject(practiceGameInfo)}");
            _ = connection.SendMessage(gameInfoNotification);
            return;
        }

        private static LobbyGameInfo CreatePracticeLobbyGameInfo()
        {
            return new LobbyGameInfo
            {
                AcceptTimeout = TimeSpan.Zero,
                GameResult = GameResult.NoResult,
                GameServerAddress = null,
                GameServerHost = null,
                GameStatus = GameStatus.Launching,
                GameServerProcessCode = "LeProcessCode",
                MonitorServerProcessCode = "",
                LoadoutSelectTimeout = TimeSpan.FromMinutes(1),
                SelectSubPhaseBan1Timeout = TimeSpan.FromMinutes(1),
                SelectSubPhaseBan2Timeout = TimeSpan.FromSeconds(30),
                SelectSubPhaseFreelancerSelectTimeout = TimeSpan.FromSeconds(30),
                SelectSubPhaseTradeTimeout = TimeSpan.FromSeconds(30),
                GameConfig = CreatePracticeLobbyGameConfig()
            };
        }

        private static LobbyGameConfig CreatePracticeLobbyGameConfig() {
            GameTypeAvailability practiceGameType = GameTypesUtils.GetPracticeGameTypeAvailability();
            return new LobbyGameConfig
            {
                GameOptionFlags = GameOptionFlag.AutoLaunch | GameOptionFlag.NoInputIdleDisconnect | GameOptionFlag.EnableTeamAIOutput,
                GameType = GameType.Practice,
                InstanceSubTypeBit = 1,
                IsActive = true,
                ResolveTimeoutLimit = 160,
                TeamAPlayers = 1,
                Map = practiceGameType.SubTypes[0].GameMapConfigs[0].Map,
                SubTypes = practiceGameType.SubTypes,
                RoomName = GenerateRoomName()
            };
        }

        private async static Task SendNotification()
        {
            foreach (var client in Instance.Queue)
            {
                MatchmakingQueueAssignmentNotification notification = new MatchmakingQueueAssignmentNotification()
                {
                    Reason = "",
                    MatchmakingQueueInfo = new LobbyMatchmakingQueueInfo()
                    {
                        ShowQueueSize = true,
                        QueuedPlayers = QueuedPlayers[client.SelectedGameType],
                        AverageWaitTime = TimeSpan.FromSeconds(100),
                        PlayersPerMinute = 0,
                        GameConfig = new LobbyGameConfig(),
                        QueueStatus = QueueStatus.QueueDoesntHaveEnoughHumans
                    }
                };

                await client.SendMessage(notification);
            }
        }*/
    }
}
