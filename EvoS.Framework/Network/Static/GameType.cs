using System;
using EvoS.Framework.Network;

// Token: 0x0200076C RID: 1900
[EvosMessage(52)]
public enum GameType
{
    // Token: 0x0400389F RID: 14495
    None = -1,
    // Token: 0x040038A0 RID: 14496
    Custom,
    // Token: 0x040038A1 RID: 14497
    Practice,
    // Token: 0x040038A2 RID: 14498
    Tutorial,
    // Token: 0x040038A3 RID: 14499
    Coop,
    // Token: 0x040038A4 RID: 14500
    PvP,
    // Token: 0x040038A5 RID: 14501
    Solo,
    // Token: 0x040038A6 RID: 14502
    Duel,
    // Token: 0x040038A7 RID: 14503
    PvE,
    // Token: 0x040038A8 RID: 14504
    LoadTestPvP,
    // Token: 0x040038A9 RID: 14505
    QuickPlay,
    // Token: 0x040038AA RID: 14506
    LoadTestSolo,
    // Token: 0x040038AB RID: 14507
    Ranked,
    // Token: 0x040038AC RID: 14508
    NewPlayerPvP,
    // Token: 0x040038AD RID: 14509
    LoadTestRanked,
    // Token: 0x040038AE RID: 14510
    NewPlayerSolo,
    // Token: 0x040038AF RID: 14511
    Casual,
    // Token: 0x040038B0 RID: 14512
    Last
}
