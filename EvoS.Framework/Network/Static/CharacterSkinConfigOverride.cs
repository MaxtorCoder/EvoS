using System;
using System.Collections.Generic;
using EvoS.Framework.Network;

// Token: 0x02000958 RID: 2392
[Serializable]
[EvosMessage(656)]
public class CharacterSkinConfigOverride
{
    // Token: 0x060042E2 RID: 17122 RVA: 0x0003834C File Offset: 0x0003654C
    public CharacterSkinConfigOverride(CharacterType characterType)
    {
        this.CharacterType = characterType;
        this.SkinConfigs = new List<SkinConfigOverride>();
    }

    // Token: 0x060042E3 RID: 17123 RVA: 0x00038366 File Offset: 0x00036566
    public CharacterSkinConfigOverride Clone()
    {
        return (CharacterSkinConfigOverride)base.MemberwiseClone();
    }

    // Token: 0x0400408C RID: 16524
    public CharacterType CharacterType;

    // Token: 0x0400408D RID: 16525
    [EvosMessage(657)]
    public List<SkinConfigOverride> SkinConfigs;
}
