using System;
using EvoS.Framework.Network;

// Token: 0x02000953 RID: 2387
[Serializable]
[EvosMessage(667)]
public class CharacterAbilityConfigOverride
{
    // Token: 0x060042D4 RID: 17108 RVA: 0x00038294 File Offset: 0x00036494
    public CharacterAbilityConfigOverride(CharacterType characterType)
    {
        this.CharacterType = characterType;
        Array.Resize<AbilityConfigOverride>(ref this.AbilityConfigs, 5);
    }

    // Token: 0x060042D5 RID: 17109 RVA: 0x000382AF File Offset: 0x000364AF
    public AbilityConfigOverride GetAbilityConfig(int abilityIndex)
    {
        if (abilityIndex < this.AbilityConfigs.Length)
        {
            return this.AbilityConfigs[abilityIndex];
        }
        return null;
    }

    // Token: 0x060042D6 RID: 17110 RVA: 0x000382C6 File Offset: 0x000364C6
    public void SetAbilityConfig(int abilityIndex, AbilityConfigOverride abilityConfig)
    {
        if (abilityIndex < this.AbilityConfigs.Length)
        {
            this.AbilityConfigs[abilityIndex] = abilityConfig;
        }
    }

    // Token: 0x060042D7 RID: 17111 RVA: 0x000382DC File Offset: 0x000364DC
    public CharacterAbilityConfigOverride Clone()
    {
        return (CharacterAbilityConfigOverride)base.MemberwiseClone();
    }

    // Token: 0x0400407C RID: 16508
    public CharacterType CharacterType;

    // Token: 0x0400407D RID: 16509
    [EvosMessage(668)]
    public AbilityConfigOverride[] AbilityConfigs;
}
