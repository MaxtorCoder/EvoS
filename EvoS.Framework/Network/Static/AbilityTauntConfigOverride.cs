using System;
using EvoS.Framework.Network;

// Token: 0x02000956 RID: 2390
[Serializable]
[EvosMessage(673)]
public class AbilityTauntConfigOverride
{
    // Token: 0x060042DF RID: 17119 RVA: 0x00038332 File Offset: 0x00036532
    public AbilityTauntConfigOverride Clone()
    {
        return (AbilityTauntConfigOverride)base.MemberwiseClone();
    }

    // Token: 0x04004083 RID: 16515
    public CharacterType CharacterType;

    // Token: 0x04004084 RID: 16516
    public int AbilityIndex;

    // Token: 0x04004085 RID: 16517
    public int AbilityTauntIndex;

    // Token: 0x04004086 RID: 16518
    public int AbilityTauntID;

    // Token: 0x04004087 RID: 16519
    public bool Allowed;
}
