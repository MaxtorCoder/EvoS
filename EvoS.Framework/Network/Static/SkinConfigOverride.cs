using System;
using EvoS.Framework.Network;

// Token: 0x02000959 RID: 2393
[Serializable]
[EvosMessage(659)]
public class SkinConfigOverride
{
    // Token: 0x060042E5 RID: 17125 RVA: 0x00038373 File Offset: 0x00036573
    public SkinConfigOverride Clone()
    {
        return (SkinConfigOverride)base.MemberwiseClone();
    }

    // Token: 0x0400408E RID: 16526
    public int SkinIndex;

    // Token: 0x0400408F RID: 16527
    public int PatternIndex;

    // Token: 0x04004090 RID: 16528
    public int ColorIndex;

    // Token: 0x04004091 RID: 16529
    public bool Allowed;
}
