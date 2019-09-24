using System;
using System.Collections.Generic;
using System.Linq;
using EvoS.Framework.Network;

// Token: 0x02000954 RID: 2388
[Serializable]
[EvosMessage(669)]
public class AbilityConfigOverride
{
    // Token: 0x060042D8 RID: 17112 RVA: 0x000382E9 File Offset: 0x000364E9
    public AbilityConfigOverride(CharacterType characterType, int abilityIndex)
    {
        this.CharacterType = characterType;
        this.AbilityIndex = abilityIndex;
    }

    // Token: 0x060042D9 RID: 17113 RVA: 0x001690AC File Offset: 0x001672AC
    public AbilityModConfigOverride GetAbilityModConfig(int index)
    {
        AbilityModConfigOverride result = null;
        this.AbilityModConfigs.TryGetValue(index, out result);
        return result;
    }

    // Token: 0x060042DA RID: 17114 RVA: 0x001690CC File Offset: 0x001672CC
    public AbilityTauntConfigOverride GetAbilityTauntConfig(int tauntId)
    {
        return this.AbilityTauntConfigs.Values.FirstOrDefault((AbilityTauntConfigOverride taunt) => taunt.AbilityTauntID == tauntId);
    }

    // Token: 0x060042DB RID: 17115 RVA: 0x00038315 File Offset: 0x00036515
    public AbilityConfigOverride Clone()
    {
        return (AbilityConfigOverride)base.MemberwiseClone();
    }

    // Token: 0x0400407E RID: 16510
    public CharacterType CharacterType;

    // Token: 0x0400407F RID: 16511
    public int AbilityIndex;

    // Token: 0x04004080 RID: 16512
    [EvosMessage(674)]
    public Dictionary<int, AbilityModConfigOverride> AbilityModConfigs = new Dictionary<int, AbilityModConfigOverride>();

    // Token: 0x04004081 RID: 16513
    [EvosMessage(670)]
    public Dictionary<int, AbilityTauntConfigOverride> AbilityTauntConfigs = new Dictionary<int, AbilityTauntConfigOverride>();
}
