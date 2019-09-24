using System;
using EvoS.Framework.Network;

// Token: 0x02000957 RID: 2391
[Serializable]
[EvosMessage(677)]
public class AbilityModConfigOverride
{
    // Token: 0x060042E1 RID: 17121 RVA: 0x0003833F File Offset: 0x0003653F
    public AbilityModConfigOverride Clone()
    {
        return (AbilityModConfigOverride)base.MemberwiseClone();
    }

    // Token: 0x04004088 RID: 16520
    public CharacterType CharacterType;

    // Token: 0x04004089 RID: 16521
    public int AbilityIndex;

    // Token: 0x0400408A RID: 16522
    public int AbilityModIndex;

    // Token: 0x0400408B RID: 16523
    public bool Allowed;
}
