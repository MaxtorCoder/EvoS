using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.NetworkMessages;
using EvoS.Framework.Network.Static;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.LobbyServer.Utils
{
    class GameTypesUtils
    {
        public class SubTypeMasks {
            public const ushort SoloVsBots = 0;
            public const ushort PlayerTeamVsBots = 1;
        }
        public static Dictionary<GameType, GameTypeAvailability> GetAvailableGameTypes() {

            Dictionary<GameType, GameTypeAvailability> availableGameTypes = new Dictionary<GameType, GameTypeAvailability>();

            availableGameTypes.Add(GameType.Practice, GetPracticeGameTypeAvailability());
            availableGameTypes.Add(GameType.Coop, GetCoopGameTypeAvailability());
            availableGameTypes.Add(GameType.PvP, GetPvPGameTypeAvailability());
            availableGameTypes.Add(GameType.Ranked, GetRankedGameTypeAvailability());
            availableGameTypes.Add(GameType.Custom, GetCustomGameTypeAvailability());

            return availableGameTypes;
        }

        public static GameTypeAvailability GetPracticeGameTypeAvailability() {
            GameTypeAvailability type = new GameTypeAvailability();
            type.MaxWillFillPerTeam = 4;
            type.IsActive = true;
            type.QueueableGroupSizes = new Dictionary<int, RequirementCollection> { { 1, null } };
            type.TeamAPlayers = 1;
            type.TeamBBots = 2;

            type.SubTypes = new List<GameSubType>()
            {
                new GameSubType {
                    LocalizedName = "GenericPractice@SubTypes",
                    GameMapConfigs = new List<GameMapConfig>{ new GameMapConfig(Maps.VR_Practice, true) },
                    RewardBucket = GameBalanceVars.GameRewardBucketType.NoRewards,
                    PersistedStatBucket = PersistedStatBucket.DoNotPersist,
                    TeamBBots = 2,
                    Mods = new List<GameSubType.SubTypeMods>
                    {
                        GameSubType.SubTypeMods.AllowPlayingLockedCharacters,
                        GameSubType.SubTypeMods.HumansHaveFirstSlots
                    },
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
                            }, {
                                TeamCompositionRules.SlotTypes.A2, new FreelancerSet{Types = new List<CharacterType> {CharacterType.PunchingDummy}}
                            }, {
                                TeamCompositionRules.SlotTypes.A4, new FreelancerSet{Types = new List<CharacterType> {CharacterType.PunchingDummy}}
                            }, {
                                TeamCompositionRules.SlotTypes.A3, new FreelancerSet{Types = new List<CharacterType> {CharacterType.PunchingDummy}}
                            }, {
                                TeamCompositionRules.SlotTypes.TeamB, new FreelancerSet{Types = new List<CharacterType> {CharacterType.PunchingDummy}}
                            }
                        }
                    }
                }
            };

            return type;
        }

        public static GameTypeAvailability GetCoopGameTypeAvailability()
        {
            GameTypeAvailability type = new GameTypeAvailability();
            type.IsActive = true;
            type.MaxWillFillPerTeam = 0;
            type.SubTypes = new List<GameSubType>() {
                new GameSubType() {
                    TeamAPlayers = 4,
                    TeamABots = 0,
                    TeamBPlayers = 0,
                    TeamBBots = 4,

                    DuplicationRule = FreelancerDuplicationRuleTypes.noneInTeam,
                    GameMapConfigs = GameMapConfig.GetDeatmatchMaps(),
                    InstructionsToDisplay = GameSubType.GameLoadScreenInstructions.Default,
                    
                }
            };
            return type;
        }

        public static GameTypeAvailability GetPvPGameTypeAvailability() {
            GameTypeAvailability type = new GameTypeAvailability();
            type.IsActive = false;
            type.MaxWillFillPerTeam = 0;
            type.SubTypes = new List<GameSubType>();
            return type;
        }

        public static GameTypeAvailability GetRankedGameTypeAvailability()
        {
            GameTypeAvailability type = new GameTypeAvailability();
            type.IsActive = false;
            type.MaxWillFillPerTeam = 0;
            type.SubTypes = new List<GameSubType>();
            return type;
        }

        public static GameTypeAvailability GetCustomGameTypeAvailability()
        {
            GameTypeAvailability type = new GameTypeAvailability();
            type.IsActive = false;
            type.MaxWillFillPerTeam = 0;
            type.SubTypes = new List<GameSubType>();
            return type;
        }
    }
}
