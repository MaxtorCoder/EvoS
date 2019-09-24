using System;
using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.NetworkMessages;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(132)]
    public class GameSubType
    {
        public GameSubType Clone()
        {
            GameSubType gameSubType = (GameSubType) MemberwiseClone();
            gameSubType.GameMapConfigs = new List<GameMapConfig>();
            foreach (GameMapConfig gameMapConfig in GameMapConfigs)
            {
                gameSubType.GameMapConfigs.Add(gameMapConfig.Clone());
            }

            return gameSubType;
        }

        public static TimeSpan ConformTurnTimeSpanFromSeconds(double totalSeconds)
        {
            double value = Math.Max(1.0, Math.Min(99.9, totalSeconds));
            return TimeSpan.FromSeconds(value);
        }

        public string LocalizedName;
        [EvosMessage(163)]
        public List<GameMapConfig> GameMapConfigs;
        [EvosMessage(154)]
        public List<SubTypeMods> Mods;
        public GameLoadScreenInstructions InstructionsToDisplay;
        public RequirementCollection Requirements;
        public Rate MaxMatchesGrantingXP;
        public TeamCompositionRules TeamComposition;
        public GameValueOverrides GameOverrides;
        public int TeamAPlayers = -1;
        public int TeamBPlayers = -1;
        public int TeamABots = -1;
        public int TeamBBots = -1;
        public FreelancerRoleBalancingRuleTypes RoleBalancingRule;
        public FreelancerDuplicationRuleTypes DuplicationRule;
        public FreelancerTieBreakerRuleTypes TiebreakerRule;
        [EvosMessage(151)]
        public List<RankedSelectionOrderType> RankedSelectionOrder;
        public PersistedStatBucket PersistedStatBucket;
        public GameBalanceVars.GameRewardBucketType RewardBucket = GameBalanceVars.GameRewardBucketType.NoRewards;
        public TimeSpan LoadoutSelectionTimeoutOverride;

        [EvosMessage(160)]
        public enum GameLoadScreenInstructions
        {
            Default,
            Extraction,
            OverpoweredUp,
            SupportalCombat,
            LightsOut,
            FourCharacters,
            NewGameLoadScreenInstructions
        }

        [EvosMessage(156)]
        public enum SubTypeMods
        {
            Exclusive,
            AntiSocial,
            OverrideFreelancerSelection,
            RankedFreelancerSelection,
            AllowPlayingLockedCharacters,
            BlockQueueMMRUpdate,
            UpdateOnlyCasualQueueMMR,
            NotCheckedByDefault,
            HumansHaveFirstSlots,
            AFKPlayersAbortPreLoadGame,
            CanBeConsolidated,
            NotAllowedForGroups,
            ShowWithAITeammates,
            ControlAllBots,
            StricterMods
        }

        [EvosMessage(153)]
        public enum RankedSelectionOrderType
        {
            Random,
            Karma,
            MMR,
            Human,
            Slot,
            Tier,
            FreelancersOwned,
            GroupLeaderTrumps
        }
    }
}
