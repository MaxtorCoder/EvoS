using System;
using EvoS.Framework.Network;

// Token: 0x02000952 RID: 2386
[Serializable]
[EvosMessage(681)]
public class CardConfigOverride
{
    // Token: 0x060042D3 RID: 17107 RVA: 0x00038287 File Offset: 0x00036487
    public CardConfigOverride Clone()
    {
        return (CardConfigOverride)base.MemberwiseClone();
    }

    // Token: 0x0400407A RID: 16506
    public CardType CardType;

    // Token: 0x0400407B RID: 16507
    public bool Allowed;
}
