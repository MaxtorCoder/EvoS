using System;
using EvoS.Framework.Network;

// Token: 0x0200095B RID: 2395
[Serializable]
[EvosMessage(650)]
public class FactionTierConfigOverride
{
    // Token: 0x060042E9 RID: 17129 RVA: 0x0003838D File Offset: 0x0003658D
    public FactionTierConfigOverride Clone()
    {
        return (FactionTierConfigOverride)base.MemberwiseClone();
    }

    // Token: 0x04004095 RID: 16533
    public int CompetitionId;

    // Token: 0x04004096 RID: 16534
    public int FactionId;

    // Token: 0x04004097 RID: 16535
    public int TierId;

    // Token: 0x04004098 RID: 16536
    public long ContributionToComplete;
}
