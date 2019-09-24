using System;
using System.Collections.Generic;
using EvoS.Framework.Network;

// Token: 0x02000951 RID: 2385
[Serializable]
[EvosMessage(663)]
public class CharacterConfig
{
    // Token: 0x060042D1 RID: 17105 RVA: 0x0003827A File Offset: 0x0003647A
    public CharacterConfig Clone()
    {
        return (CharacterConfig)base.MemberwiseClone();
    }

    // Token: 0x04004072 RID: 16498
    public CharacterType CharacterType;

    // Token: 0x04004073 RID: 16499
    public CharacterRole CharacterRole;

    // Token: 0x04004074 RID: 16500
    public bool AllowForBots;

    // Token: 0x04004075 RID: 16501
    public bool AllowForPlayers;

    // Token: 0x04004076 RID: 16502
    public bool IsHidden;

    // Token: 0x04004077 RID: 16503
    public DateTime IsHiddenFromFreeRotationUntil;

    // Token: 0x04004078 RID: 16504
    public List<GameType> GameTypesProhibitedFrom;

    // Token: 0x04004079 RID: 16505
    public int Difficulty;
}
