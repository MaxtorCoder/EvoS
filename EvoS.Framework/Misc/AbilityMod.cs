using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    public class AbilityMod : MonoBehaviour
    {
        public string m_name = string.Empty;
        public bool m_availableInGame = true;
        public int m_equipCost = 1;
         public string m_tooltip = string.Empty;
        public string m_flavorText = string.Empty;
        public string m_debugUnlocalizedTooltip = string.Empty;
        public AbilityPriority m_runPriorityOverride = AbilityPriority.Combat_Damage;
        public Ability.MovementAdjustment m_movementAdjustmentOverride = Ability.MovementAdjustment.ReducedMovement;

//        [Header("    The above effects use this 0-1 value")]
        public float m_effectTriggerChance = 1f;

        public List<AbilityTags> m_abilityTagsInMod = new List<AbilityTags>();
        public List<StatusType> m_statusWhenRequestedOverride = new List<StatusType>();

//        [Header("-- ID (only need to be unique among mods of the same ability)")]
        public int m_abilityScopeId;

        public AbilityModGameTypeReq m_gameTypeReq;
        public bool m_defaultEquip;
        public List<StatusType> m_savedStatusTypesForTooltips;
//        public Sprite m_iconSprite;

//        [Separator("Run Priority Mod", "orange")]
        public bool m_useRunPriorityOverride;

//        [Separator("Energy Cost, Cooldown/Stock", "orange")]
        public AbilityModPropertyInt m_techPointCostMod;

//        [Header("-- Cooldown / Stock")] public AbilityModPropertyInt m_maxCooldownMod;
        public AbilityModPropertyInt m_maxStocksMod;
        public AbilityModPropertyInt m_stockRefreshDurationMod;
        public AbilityModPropertyBool m_refillAllStockOnRefreshMod;
//        [Header("-- Free Action Override")]
         public AbilityModPropertyBool m_isFreeActionMod;
//        [Header("-- Auto Queue Override")]
         public AbilityModPropertyBool m_autoQueueIfValidMod;

//        [Separator("Targeter Range Mods (applies to all entries in Target Data)", "orange")]
        public AbilityModPropertyFloat m_targetDataMaxRangeMod;

        public AbilityModPropertyFloat m_targetDataMinRangeMod;
        public AbilityModPropertyBool m_targetDataCheckLosMod;
//        [Header("    Target Data override")]
        public bool m_useTargetDataOverrides;
        public TargetData[] m_targetDataOverrides;

//        [Separator("Tech Point Interaction", "orange")]
        public TechPointInteractionMod[] m_techPointInteractionMods;

//        [Header("-- Anim type override")] 
        public bool m_useActionAnimTypeOverride;
        public ActorModelData.ActionAnimationType m_actionAnimTypeOverride;

//        [Header("-- Movement Adjustment Override")]
        public bool m_useMovementAdjustmentOverride;

//        [Header("-- Effect to Self / Targeted Enemies or Allies")]
        public StandardEffectInfo m_effectToSelfOnCast;

        public StandardEffectInfo m_effectToTargetEnemyOnHit;

//        [Tooltip("NOTE: the ability doesn't automatically make allies targetable for this stuff. Each ability needs to do that if allies are only targetable with certain mods.")]
        public StandardEffectInfo m_effectToTargetAllyOnHit;

//        [Tooltip("This would be in addition to the Effect To Self On Cast if both are set. Use mainly for single-target abilities.")]
        public bool m_useAllyEffectForTargetedCaster;

//        [Header("    If you want more hits to increase the chance")]
        public bool m_effectTriggerChanceMultipliedPerHit;

//        [Header("-- Cooldown Reductions on Cast")]
        public AbilityModCooldownReduction m_cooldownReductionsOnSelf;

//        [Header("-- Sequence for self hit timing (for effect/cooldown reduction). If empty, will use placeholder")]
        public GameObject m_selfHitTimingSequencePrefab;

//        [Header("-- Chain Ability's Additional Effects/Cooldown Reductions")]
        public List<ChainAbilityAdditionalModInfo> m_chainAbilityModInfo;

//        [Header("    Chain Ability override")] public bool m_useChainAbilityOverrides;
        public Ability[] m_chainAbilityOverrides;
//        [Header("-- Ability Tag Override")] public AbilityMod.TagOverrideType m_tagsModType;

//        [Header("-- Stat Mods While Mod is Equipped")]
        public AbilityStatMod[] m_statModsWhileEquipped;

//        [Header("-- Buff/Debuff Status when ability is requested (apply in Decision)")]
        public bool m_useStatusWhenRequestedOverride;

//        [Space(25f)] [Separator("End of Base Ability Mod", "orange")]
        public bool beginningOfModSpecificData;

        public virtual System.Type GetTargetAbilityType()
        {
            return typeof(Ability);
        }

        public enum TagOverrideType
        {
            Ignore,
            Override,
            Append,
        }
    }
}
