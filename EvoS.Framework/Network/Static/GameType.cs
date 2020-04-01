using System;
using EvoS.Framework.Network;

// Token: 0x0200076C RID: 1900
[EvosMessage(52)]
public enum GameType
{
    None = -1,
    Custom,
    Practice,
    Tutorial,
    Coop,
    PvP,
    Solo,
    Duel,
    PvE,
    LoadTestPvP,
    QuickPlay,
    LoadTestSolo,
    Ranked,
    NewPlayerPvP,
    LoadTestRanked,
    NewPlayerSolo,
    Casual,
    Last
}
