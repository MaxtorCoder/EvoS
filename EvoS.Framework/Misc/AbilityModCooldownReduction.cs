using System;
using System.Collections.Generic;

namespace EvoS.Framework.Misc
{
[Serializable]
public class AbilityModCooldownReduction
{
  public AbilityData.ActionType m_onAbility = AbilityData.ActionType.INVALID_ACTION;
  public List<AbilityData.ActionType> m_additionalAbilities = new List<AbilityData.ActionType>();
  public int m_maxReduction = -1;
//  [Header("[Stock/Charge Refresh]")]
  public List<AbilityData.ActionType> m_stockAbilities = new List<AbilityData.ActionType>();
  public AbilityModCooldownReduction.ModAmountType m_modAmountType;
//  [Header("[Flat => baseVal + finalAdd], [Mult => Round(baseVal*numTargets) + finalAdd]")]
  public float m_baseValue;
  public float m_finalAdd;
  public int m_minReduction;
  public int m_stockBaseValue;
  public int m_stockFinalAdd;
  public int m_refreshProgressBaseValue;
  public int m_refreshProgressFinalAdd;
  public bool m_resetRefreshProgress;

  public enum ModAmountType
  {
    FlatOnCast,
    FlatOnAnyNonSelfHit,
    FlatOnAnyAllyHit,
    FlatOnAnyEnemyHit,
    MultPerAllyHit,
    MultPerEnemyHit,
    FlatOnNoEnemyHit,
    FlatOnNoAllyHit,
    FlatOnNoNonCasterHit,
  }
}
}
