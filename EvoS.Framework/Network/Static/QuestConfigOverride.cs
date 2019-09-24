using System;
using EvoS.Framework.Network;

// Token: 0x0200095A RID: 2394
[Serializable]
[EvosMessage(643)]
public class QuestConfigOverride
{
    // Token: 0x060042E7 RID: 17127 RVA: 0x00038380 File Offset: 0x00036580
    public QuestConfigOverride Clone()
    {
        return (QuestConfigOverride)base.MemberwiseClone();
    }

    // Token: 0x04004092 RID: 16530
    public int Index;

    // Token: 0x04004093 RID: 16531
    public bool Enabled;

    // Token: 0x04004094 RID: 16532
    public bool ShouldAbandon;
}
