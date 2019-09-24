using System;
using System.Collections.Generic;
using EvoS.Framework.Network;

// Token: 0x0200095C RID: 2396
[Serializable]
[EvosMessage(647)]
public class FactionCompetitionConfigOverride
{
    // Token: 0x060042EB RID: 17131 RVA: 0x0003839A File Offset: 0x0003659A
    public FactionCompetitionConfigOverride Clone()
    {
        return (FactionCompetitionConfigOverride)base.MemberwiseClone();
    }

    // Token: 0x04004099 RID: 16537
    public int Index;

    // Token: 0x0400409A RID: 16538
    public string InternalName;

    // Token: 0x0400409B RID: 16539
    [EvosMessage(648)]
    public List<FactionTierConfigOverride> FactionTierConfigs;
}
