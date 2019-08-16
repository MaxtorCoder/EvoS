using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using UnityEngine;

// Token: 0x0200095D RID: 2397
[Serializable]
public class LobbyGameplayOverrides
{
    // Token: 0x0400409C RID: 16540
    public Dictionary<CharacterType, CharacterConfig> CharacterConfigOverrides = new Dictionary<CharacterType, CharacterConfig>(default(CharacterTypeComparer));

    // Token: 0x0400409D RID: 16541
    public Dictionary<CharacterType, CharacterConfig> CharacterConfigs = new Dictionary<CharacterType, CharacterConfig>(default(CharacterTypeComparer));

    // Token: 0x0400409E RID: 16542
    public Dictionary<CardType, CardConfigOverride> CardConfigOverrides = new Dictionary<CardType, CardConfigOverride>();

    // Token: 0x0400409F RID: 16543
    public Dictionary<CharacterType, CharacterAbilityConfigOverride> CharacterAbilityConfigOverrides = new Dictionary<CharacterType, CharacterAbilityConfigOverride>();

    // Token: 0x040040A0 RID: 16544
    public Dictionary<CharacterType, CharacterSkinConfigOverride> CharacterSkinConfigOverrides = new Dictionary<CharacterType, CharacterSkinConfigOverride>();

    // Token: 0x040040A1 RID: 16545
    public Dictionary<int, QuestConfigOverride> QuestConfigOverrides = new Dictionary<int, QuestConfigOverride>();

    // Token: 0x040040A2 RID: 16546
    public Dictionary<int, FactionCompetitionConfigOverride> FactionCompetitionConfigOverrides = new Dictionary<int, FactionCompetitionConfigOverride>();

    // Token: 0x040040A3 RID: 16547
    public List<string> DisabledMaps = new List<string>();

    // Token: 0x040040A4 RID: 16548
    public List<GameType> DisabledGameTypes = new List<GameType>();

    // Token: 0x040040A5 RID: 16549
    public bool EnableMods = true;

    // Token: 0x040040A6 RID: 16550
    public bool EnableCards = true;

    // Token: 0x040040A7 RID: 16551
    public bool EnableTaunts = true;

    // Token: 0x040040A8 RID: 16552
    public bool EnableQuests = true;

    // Token: 0x040040A9 RID: 16553
    public bool EnableHiddenCharacters;

    // Token: 0x040040AA RID: 16554
    public bool RankedUpdatesEnabled = true;

    // Token: 0x040040AB RID: 16555
    public bool EnableAllMods;

    // Token: 0x040040AC RID: 16556
    public bool EnableAllAbilityVfxSwaps;

    // Token: 0x040040AD RID: 16557
    public bool EnableShop = true;

    // Token: 0x040040AE RID: 16558
    public bool EnableSeasons = true;

    // Token: 0x040040AF RID: 16559
    public bool EnableDiscord;

    // Token: 0x040040B0 RID: 16560
    public bool EnableDiscordSdk;

    // Token: 0x040040B1 RID: 16561
    public bool EnableFacebook;

    // Token: 0x040040B2 RID: 16562
    public bool EnableConversations = true;

    // Token: 0x040040B3 RID: 16563
    public bool EnableEventBonus = true;

    // Token: 0x040040B4 RID: 16564
    public bool EnableClientPerformanceCollecting;

    // Token: 0x040040B5 RID: 16565
    public bool EnableSteamAchievements;

    // Token: 0x040040B6 RID: 16566
    public bool AllowSpectators = true;

    // Token: 0x040040B7 RID: 16567
    public bool AllowSpectatorsOutsideCustom;

    // Token: 0x040040B8 RID: 16568
    public bool AllowReconnectingToGameInstantly;

    // Token: 0x040040B9 RID: 16569
    public int SpectatorOutsideCustomTurnDelay = 2;

    // Token: 0x040040BA RID: 16570
    public bool SoloGameNoAutoLockinOnTimeout = true;

    // Token: 0x040040BB RID: 16571
    public bool UseSpectatorReplays;

    // Token: 0x040040BC RID: 16572
    public bool DisableControlPadInput;

    // Token: 0x040040BD RID: 16573
    public bool UseFakeGameServersForLoadTests;

    // Token: 0x040040BE RID: 16574
    public bool UseFakeClientConnectionsForLoadTests = true;

    // Token: 0x040040BF RID: 16575
    public int LoadTestClients = 10;

    // Token: 0x040040C0 RID: 16576
    public int LoadTestMaxClientsPerInstance = 10;

    // Token: 0x040040C1 RID: 16577
    public double LoadTestLoginRate = 1.0;

    // Token: 0x040040C2 RID: 16578
    public double LoadTestLobbyDelay;

    // Token: 0x040040C3 RID: 16579
    public double LoadTestReadyDelay;

    // Token: 0x040040C4 RID: 16580
    public int LoadTestSoloGamePercentage;

    // Token: 0x040040C5 RID: 16581
    public int LoadTestGroupingPercentage;

    // Token: 0x040040C6 RID: 16582
    public string LoadTestFakeEntitlements = "GAME_ACCESS";

    // Token: 0x040040C7 RID: 16583
    public DateTime PlayerPenaltyAmnesty = DateTime.MinValue;

    // Token: 0x040040C8 RID: 16584
    public int EventFreePlayerXPBonusPercent;

    // Token: 0x040040C9 RID: 16585
    public int EventPaidPlayerXPBonusPercent;

    // Token: 0x040040CA RID: 16586
    public int EventISOBonusPercent;

    // Token: 0x040040CB RID: 16587
    public int EventGGBoostBonusPercent;

    // Token: 0x040040CC RID: 16588
    public int EventTrustInfluenceBonusPercent;

    // Token: 0x040040CD RID: 16589
    public int EventFreelancerCurrencyPerMatchBonusPercent;

    // Token: 0x040040CE RID: 16590
    public DateTime EventBonusStartDate = DateTime.MinValue;

    // Token: 0x040040CF RID: 16591
    public DateTime EventBonusEndDate = DateTime.MaxValue;

    // Token: 0x040040D0 RID: 16592
    public string RequiredEventBonusEntitlement;

    // Token: 0x040040D1 RID: 16593
    public CharacterType ForcedFreeRotationCharacterForGroupA;

    // Token: 0x040040D2 RID: 16594
    public CharacterType ForcedFreeRotationCharacterForGroupB;

    // Token: 0x040040D3 RID: 16595
    public TimeSpan ClientPerformanceCollectingFrequency = TimeSpan.FromMinutes(5.0);

    // Token: 0x040040D4 RID: 16596
    public TimeSpan RankedLeaderboardExpirationTime = TimeSpan.MaxValue;
}
