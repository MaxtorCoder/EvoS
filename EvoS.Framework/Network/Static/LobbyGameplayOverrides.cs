using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using UnityEngine;

// Token: 0x0200095D RID: 2397
[Serializable]
public class LobbyGameplayOverrides
{
    public bool AllowReconnectingToGameInstantly;
    public bool AllowSpectators = true;
    public bool AllowSpectatorsOutsideCustom;
    public Dictionary<CardType, CardConfigOverride> CardConfigOverrides = new Dictionary<CardType, CardConfigOverride>();
    public Dictionary<CharacterType, CharacterAbilityConfigOverride> CharacterAbilityConfigOverrides = new Dictionary<CharacterType, CharacterAbilityConfigOverride>();
    public Dictionary<CharacterType, CharacterConfig> CharacterConfigOverrides = new Dictionary<CharacterType, CharacterConfig>(default(CharacterTypeComparer));
    public Dictionary<CharacterType, CharacterConfig> CharacterConfigs = new Dictionary<CharacterType, CharacterConfig>(default(CharacterTypeComparer));
    public Dictionary<CharacterType, CharacterSkinConfigOverride> CharacterSkinConfigOverrides = new Dictionary<CharacterType, CharacterSkinConfigOverride>();
    public TimeSpan ClientPerformanceCollectingFrequency = TimeSpan.FromMinutes(5.0);
    public bool DisableControlPadInput;
    public List<GameType> DisabledGameTypes = new List<GameType>();
    public List<string> DisabledMaps = new List<string>();
    public bool EnableAllAbilityVfxSwaps;
    public bool EnableAllMods;
    public bool EnableCards = true;
    public bool EnableClientPerformanceCollecting;
    public bool EnableConversations = true;
    public bool EnableDiscord;
    public bool EnableDiscordSdk;
    public bool EnableEventBonus = true;
    public bool EnableFacebook;
    public bool EnableHiddenCharacters;
    public bool EnableMods = true;
    public bool EnableQuests = true;
    public bool EnableSeasons = true;
    public bool EnableShop = true;
    public bool EnableSteamAchievements;
    public bool EnableTaunts = true;
    public DateTime EventBonusEndDate = DateTime.MaxValue;
    public DateTime EventBonusStartDate = DateTime.MinValue;
    public int EventFreePlayerXPBonusPercent;
    public int EventFreelancerCurrencyPerMatchBonusPercent;
    public int EventGGBoostBonusPercent;
    public int EventISOBonusPercent;
    public int EventPaidPlayerXPBonusPercent;
    public int EventTrustInfluenceBonusPercent;
    public Dictionary<int, FactionCompetitionConfigOverride> FactionCompetitionConfigOverrides = new Dictionary<int, FactionCompetitionConfigOverride>();
    public CharacterType ForcedFreeRotationCharacterForGroupA;
    public CharacterType ForcedFreeRotationCharacterForGroupB;
    public int LoadTestClients = 10;
    public string LoadTestFakeEntitlements = "GAME_ACCESS";
    public int LoadTestGroupingPercentage;
    public double LoadTestLobbyDelay;
    public double LoadTestLoginRate = 1.0;
    public int LoadTestMaxClientsPerInstance = 10;
    public double LoadTestReadyDelay;
    public int LoadTestSoloGamePercentage;
    public DateTime PlayerPenaltyAmnesty = DateTime.MinValue;
    public Dictionary<int, QuestConfigOverride> QuestConfigOverrides = new Dictionary<int, QuestConfigOverride>();
    public TimeSpan RankedLeaderboardExpirationTime = TimeSpan.MaxValue;
    public bool RankedUpdatesEnabled = true;
    public string RequiredEventBonusEntitlement;
    public bool SoloGameNoAutoLockinOnTimeout = true;
    public int SpectatorOutsideCustomTurnDelay = 2;
    public bool UseFakeClientConnectionsForLoadTests = true;
    public bool UseFakeGameServersForLoadTests;
    public bool UseSpectatorReplays;
}
