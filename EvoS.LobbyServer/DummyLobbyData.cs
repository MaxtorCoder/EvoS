using System;
using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.NetworkMessages;
using EvoS.Framework.Network.Static;

namespace EvoS.LobbyServer
{
    public class DummyLobbyData
    {
        public static List<PersistedCharacterData> CreateCharacterDataList()
        {
            var data = new List<PersistedCharacterData>();

            foreach (CharacterType characterType in Enum.GetValues(typeof(CharacterType)))
            {
                var characterData = new PersistedCharacterData(characterType)
                {
                    CharacterComponent = {Unlocked = true}
                };
                data.Add(characterData);
            }

            return data;
        }


        public static ServerQueueConfigurationUpdateNotification CreateServerQueueConfiguration()
        {
            return new ServerQueueConfigurationUpdateNotification
            {
                FreeRotationAdditions = new Dictionary<CharacterType, RequirementCollection>(),
                GameTypeAvailabilies = new Dictionary<GameType, GameTypeAvailability>
                {
                    {
                        GameType.Practice, new GameTypeAvailability
                        {
                            QueueableGroupSizes = new Dictionary<int, RequirementCollection> {{1, null}},
                            IsActive = true,
                            TeamAPlayers = 1,
                            SubTypes = CreatePracticeGameSubTypes()
                        }
                    },
                    {
                        GameType.Coop, new GameTypeAvailability
                        {
                            IsActive = false
                        }
                    },
                    {
                        GameType.Ranked, new GameTypeAvailability
                        {
                            IsActive = false
                        }
                    },
                    {
                        GameType.PvP, new GameTypeAvailability
                        {
                            QueueableGroupSizes = new Dictionary<int, RequirementCollection>
                            {
                                {1, null},
                                {2, null},
                                {3, null},
                                {4, null},
                            },
                            IsActive = false,
                            TeamAPlayers = 4,
                            TeamBPlayers = 4,
                            BlockedExperienceAlternativeGameType = GameType.NewPlayerPvP,
                            BlockedExperienceEntries = new List<MatchmakingQueueConfig.QueueEntryExperience> {0},
                            GameLeavingPenalty = new GameLeavingPenalty
                            {
                                PointsGainedForLeaving = 2,
                                PointsForgivenForRejoining = 1,
                                PointsForgivenPerCompleteGameFinished = 1
                            },
                            Requirements = new RequirementCollection(),
                            SubTypes = new List<GameSubType>
                            {
                                new GameSubType
                                {
                                    LocalizedName = "GenericPvP@SubTypes",
                                    GameMapConfigs = new List<GameMapConfig>
                                    {
                                        new GameMapConfig
                                        {
                                            IsActive = true,
                                            Map = "CargoShip_Deathmatch"
                                        }
                                    },
                                    MaxMatchesGrantingXP = new Rate(),
                                    TeamAPlayers = -1,
                                    TeamBPlayers = -1,
                                    TeamABots = -1,
                                    TeamBBots = -1,
                                    PersistedStatBucket = PersistedStatBucket.Deathmatch_Unranked,
                                    LoadoutSelectionTimeoutOverride = TimeSpan.Zero,
                                }
                            }
                        }
                    }
                },
                TierInstanceNames = new List<LocalizationPayload>()
            };
        }

        private static List<GameSubType> CreatePracticeGameSubTypes()
        {
            return new List<GameSubType>
            {
                new GameSubType
                {
                    LocalizedName = "GenericPractice@SubTypes",
                    GameMapConfigs = new List<GameMapConfig>
                    {
                        new GameMapConfig
                        {
                            IsActive = true,
                            Map = "VR_Practice"
                        }
                    },
                    Mods = new List<GameSubType.SubTypeMods>
                    {
                        GameSubType.SubTypeMods.AllowPlayingLockedCharacters,
                        GameSubType.SubTypeMods.HumansHaveFirstSlots
                    },
                    RewardBucket = GameBalanceVars.GameRewardBucketType.NoRewards,
                    PersistedStatBucket = PersistedStatBucket.DoNotPersist,
                    TeamABots = -1,
                    TeamBBots = -1,
                    TeamAPlayers = -1,
                    TeamBPlayers = -1,
                    TeamComposition = new TeamCompositionRules
                    {
                        Rules = new Dictionary<TeamCompositionRules.SlotTypes, FreelancerSet>
                        {
                            {
                                TeamCompositionRules.SlotTypes.A1, new FreelancerSet
                                {
                                    Roles = new List<CharacterRole>
                                    {
                                        CharacterRole.Tank,
                                        CharacterRole.Assassin,
                                        CharacterRole.Support
                                    }
                                }
                            },
                            {
                                TeamCompositionRules.SlotTypes.A2, new FreelancerSet
                                    {Types = new List<CharacterType> {CharacterType.PunchingDummy}}
                            },
                            {
                                TeamCompositionRules.SlotTypes.A4, new FreelancerSet
                                    {Types = new List<CharacterType> {CharacterType.PunchingDummy}}
                            },
                            {
                                TeamCompositionRules.SlotTypes.A3, new FreelancerSet
                                    {Types = new List<CharacterType> {CharacterType.PunchingDummy}}
                            },
                            {
                                TeamCompositionRules.SlotTypes.TeamB, new FreelancerSet
                                    {Types = new List<CharacterType> {CharacterType.PunchingDummy}}
                            }
                        }
                    }
                }
            };
        }

        public static LobbyCharacterInfo CreateLobbyCharacterInfo(CharacterType characterType)
        {
            return new LobbyCharacterInfo
            {
                CharacterType = characterType,
                CharacterSkin = new CharacterVisualInfo(),
                CharacterCards = new CharacterCardInfo().Reset(),
                CharacterMods = new CharacterModInfo().Reset(),
                CharacterAbilityVfxSwaps = new CharacterAbilityVfxSwapInfo(),
                CharacterTaunts = new List<PlayerTauntData>(),
                CharacterLoadouts = CreateCharacterLoadouts()
            };
        }

        private static List<CharacterLoadout> CreateCharacterLoadouts()
        {
            return new List<CharacterLoadout>
            {
                new CharacterLoadout(new CharacterModInfo().Reset(), new CharacterAbilityVfxSwapInfo(), "Loadout #1"),
                new CharacterLoadout(new CharacterModInfo().Reset(), new CharacterAbilityVfxSwapInfo(), "Loadout #2")
            };
        }

        public static Dictionary<CharacterType, CharacterConfig> CreateCharacterConfigs()
        {
            var data = new Dictionary<CharacterType, CharacterConfig>();

            foreach (CharacterType characterType in Enum.GetValues(typeof(CharacterType)))
            {
                data.Add(characterType, new CharacterConfig
                {
                    CharacterType = characterType,
                    Difficulty = 1,
                    CharacterRole = CharacterRole.Assassin,
                    IsHidden = false,
                    AllowForBots = true,
                    AllowForPlayers = true,
                    IsHiddenFromFreeRotationUntil = DateTime.UnixEpoch
                });
            }

            return data;
        }

        public static PersistedAccountData CreateAccountData(ClientConnection connection)
        {
            return new PersistedAccountData
            {
                QuestComponent = new QuestComponent {ActiveSeason = 9},
                AccountId = connection.AuthInfo.AccountId,
                UserName = connection.AuthInfo.UserName,
                Handle = connection.AuthInfo.Handle,
                SchemaVersion = new SchemaVersion<AccountSchemaChange>(0x1FFFF),
                CreateDate = DateTime.Now.AddHours(-1),
                UpdateDate = DateTime.Now,
            };
        }

        public static LobbyGameplayOverrides CreateLobbyGameplayOverrides()
        {
            return new LobbyGameplayOverrides
            {
                CharacterConfigs = CreateCharacterConfigs(),
                EnableHiddenCharacters = true,
                EnableAllMods = true,
                EnableQuests = false,
                EnableSeasons = false
            };
        }
    }
}
