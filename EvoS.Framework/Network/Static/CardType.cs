using System;
using EvoS.Framework.Network;


[EvosMessage(542)]
public enum CardType
{
    None = -1,
    NoOverride,
    BuffEnhancer = 26,
    Cleanse = 1,
    Cleanse_Prep = 32,
    Crippler = 2,
    Cryoshell,
    DashToEnemyAndChase = 25,
    DebuffSingleSquareScramble = 29,
    EnergyLeachAoE = 4,
    EnergyGainOnEnemyHitAoE = 28,
    Flash = 5,
    FlashAndRoot = 22,
    FlashAndStealth,
    FlashToAlly,
    KnockbackAroundSelf = 27,
    RadiationInjection = 6,
    ReduceCooldown,
    ReduceCooldown_Prep = 30,
    RevealArea = 20,
    SecondWind = 8,
    SecondWind_Prep = 31,
    StarterAbilityIncrease = 9,
    StarterCritShot,
    StarterEnergyAoE,
    StarterEnergyRegen,
    StarterHealAoE,
    StarterHealthIncrease,
    StarterHealthRegen,
    StarterMovementIncrease,
    Tether,
    TurtleTech,
    Unstoppable = 21,
    Vampirism = 19
}
