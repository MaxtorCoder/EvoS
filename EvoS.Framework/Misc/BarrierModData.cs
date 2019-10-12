using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("BarrierModData")]
    public class BarrierModData : ISerializedItem
    {
        public AbilityModPropertyInt m_durationMod;
        public AbilityModPropertyFloat m_widthMod;
        public AbilityModPropertyBool m_bidirectionalMod;
        public AbilityModPropertyInt m_maxHitsMod;
        public AbilityModPropertyBlockingRules m_blocksVisionMod;
        public AbilityModPropertyBlockingRules m_blocksAbilitiesMod;
        public AbilityModPropertyBlockingRules m_blocksMovementMod;
        public AbilityModPropertyBlockingRules m_blocksMovementOnCrossoverMod;
        public AbilityModPropertyBlockingRules m_blocksPositionTargetingMod;
        public AbilityModPropertyBool m_considerAsCoverMod;
        public AbilityModPropertyInt m_enemyMoveThroughDamageMod;
        public AbilityModPropertyInt m_enemyMoveThroughEnergyMod;
        public AbilityModPropertyEffectInfo m_enemyMoveThroughEffectMod;
        public AbilityModPropertyInt m_allyMoveThroughHealMod;
        public AbilityModPropertyInt m_allyMoveThroughEnergyMod;
        public AbilityModPropertyEffectInfo m_allyMoveThroughEffectMod;
        public AbilityModPropertyBool m_removeOnTurnEndIfEnemyCrossed;
        public AbilityModPropertyBool m_removeOnTurnEndIfAllyCrossed;
        public AbilityModPropertyBool m_removeOnPhaseEndIfEnemyCrossed;
        public AbilityModPropertyBool m_removeOnPhaseEndIfAllyCrossed;
        public AbilityModPropertyBool m_removeOnCasterDeath;
        public AbilityModPropertyInt m_healOnOwnerForShotBlock;
        public AbilityModPropertyInt m_energyGainOnOwnerForShotBlock;
        public AbilityModPropertyEffectInfo m_effectOnOwnerForShotBlock;
        public AbilityModPropertyInt m_damageOnEnemyForShotBlock;
        public AbilityModPropertyInt m_energyLossOnEnemyForShotBlock;
        public AbilityModPropertyEffectInfo m_effectOnEnemyForShotBlock;
        public AbilityModPropertySequenceOverride m_onEnemyCrossHitSequenceOverride;
        public AbilityModPropertySequenceOverride m_onAllyCrossHitSequenceOverride;
        public bool m_useBarrierSequenceOverride;
        public SerializedVector<SerializedGameObject> m_barrierSequenceOverrides;
        public AbilityModPropertySequenceOverride m_responseOnShotSequenceOverride;
        public AbilityModPropertyBool m_useShooterPosAsReactionSequenceTargetPosMod;

        public BarrierModData()
        {
        }

        public BarrierModData(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_durationMod = new AbilityModPropertyInt(assetFile, stream);
            m_widthMod = new AbilityModPropertyFloat(assetFile, stream);
            m_bidirectionalMod = new AbilityModPropertyBool(assetFile, stream);
            m_maxHitsMod = new AbilityModPropertyInt(assetFile, stream);
            m_blocksVisionMod = new AbilityModPropertyBlockingRules(assetFile, stream);
            m_blocksAbilitiesMod = new AbilityModPropertyBlockingRules(assetFile, stream);
            m_blocksMovementMod = new AbilityModPropertyBlockingRules(assetFile, stream);
            m_blocksMovementOnCrossoverMod = new AbilityModPropertyBlockingRules(assetFile, stream);
            m_blocksPositionTargetingMod = new AbilityModPropertyBlockingRules(assetFile, stream);
            m_considerAsCoverMod = new AbilityModPropertyBool(assetFile, stream);
            m_enemyMoveThroughDamageMod = new AbilityModPropertyInt(assetFile, stream);
            m_enemyMoveThroughEnergyMod = new AbilityModPropertyInt(assetFile, stream);
            m_enemyMoveThroughEffectMod = new AbilityModPropertyEffectInfo(assetFile, stream);
            m_allyMoveThroughHealMod = new AbilityModPropertyInt(assetFile, stream);
            m_allyMoveThroughEnergyMod = new AbilityModPropertyInt(assetFile, stream);
            m_allyMoveThroughEffectMod = new AbilityModPropertyEffectInfo(assetFile, stream);
            m_removeOnTurnEndIfEnemyCrossed = new AbilityModPropertyBool(assetFile, stream);
            m_removeOnTurnEndIfAllyCrossed = new AbilityModPropertyBool(assetFile, stream);
            m_removeOnPhaseEndIfEnemyCrossed = new AbilityModPropertyBool(assetFile, stream);
            m_removeOnPhaseEndIfAllyCrossed = new AbilityModPropertyBool(assetFile, stream);
            m_removeOnCasterDeath = new AbilityModPropertyBool(assetFile, stream);
            m_healOnOwnerForShotBlock = new AbilityModPropertyInt(assetFile, stream);
            m_energyGainOnOwnerForShotBlock = new AbilityModPropertyInt(assetFile, stream);
            m_effectOnOwnerForShotBlock = new AbilityModPropertyEffectInfo(assetFile, stream);
            m_damageOnEnemyForShotBlock = new AbilityModPropertyInt(assetFile, stream);
            m_energyLossOnEnemyForShotBlock = new AbilityModPropertyInt(assetFile, stream);
            m_effectOnEnemyForShotBlock = new AbilityModPropertyEffectInfo(assetFile, stream);
            m_onEnemyCrossHitSequenceOverride = new AbilityModPropertySequenceOverride(assetFile, stream);
            m_onAllyCrossHitSequenceOverride = new AbilityModPropertySequenceOverride(assetFile, stream);
            m_useBarrierSequenceOverride = stream.ReadBoolean();
            stream.AlignTo();
            m_barrierSequenceOverrides = new SerializedVector<SerializedGameObject>(assetFile, stream);
            m_responseOnShotSequenceOverride = new AbilityModPropertySequenceOverride(assetFile, stream);
            m_useShooterPosAsReactionSequenceTargetPosMod = new AbilityModPropertyBool(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(BarrierModData)}>(" +
                   $"{nameof(m_durationMod)}: {m_durationMod}, " +
                   $"{nameof(m_widthMod)}: {m_widthMod}, " +
                   $"{nameof(m_bidirectionalMod)}: {m_bidirectionalMod}, " +
                   $"{nameof(m_maxHitsMod)}: {m_maxHitsMod}, " +
                   $"{nameof(m_blocksVisionMod)}: {m_blocksVisionMod}, " +
                   $"{nameof(m_blocksAbilitiesMod)}: {m_blocksAbilitiesMod}, " +
                   $"{nameof(m_blocksMovementMod)}: {m_blocksMovementMod}, " +
                   $"{nameof(m_blocksMovementOnCrossoverMod)}: {m_blocksMovementOnCrossoverMod}, " +
                   $"{nameof(m_blocksPositionTargetingMod)}: {m_blocksPositionTargetingMod}, " +
                   $"{nameof(m_considerAsCoverMod)}: {m_considerAsCoverMod}, " +
                   $"{nameof(m_enemyMoveThroughDamageMod)}: {m_enemyMoveThroughDamageMod}, " +
                   $"{nameof(m_enemyMoveThroughEnergyMod)}: {m_enemyMoveThroughEnergyMod}, " +
                   $"{nameof(m_enemyMoveThroughEffectMod)}: {m_enemyMoveThroughEffectMod}, " +
                   $"{nameof(m_allyMoveThroughHealMod)}: {m_allyMoveThroughHealMod}, " +
                   $"{nameof(m_allyMoveThroughEnergyMod)}: {m_allyMoveThroughEnergyMod}, " +
                   $"{nameof(m_allyMoveThroughEffectMod)}: {m_allyMoveThroughEffectMod}, " +
                   $"{nameof(m_removeOnTurnEndIfEnemyCrossed)}: {m_removeOnTurnEndIfEnemyCrossed}, " +
                   $"{nameof(m_removeOnTurnEndIfAllyCrossed)}: {m_removeOnTurnEndIfAllyCrossed}, " +
                   $"{nameof(m_removeOnPhaseEndIfEnemyCrossed)}: {m_removeOnPhaseEndIfEnemyCrossed}, " +
                   $"{nameof(m_removeOnPhaseEndIfAllyCrossed)}: {m_removeOnPhaseEndIfAllyCrossed}, " +
                   $"{nameof(m_removeOnCasterDeath)}: {m_removeOnCasterDeath}, " +
                   $"{nameof(m_healOnOwnerForShotBlock)}: {m_healOnOwnerForShotBlock}, " +
                   $"{nameof(m_energyGainOnOwnerForShotBlock)}: {m_energyGainOnOwnerForShotBlock}, " +
                   $"{nameof(m_effectOnOwnerForShotBlock)}: {m_effectOnOwnerForShotBlock}, " +
                   $"{nameof(m_damageOnEnemyForShotBlock)}: {m_damageOnEnemyForShotBlock}, " +
                   $"{nameof(m_energyLossOnEnemyForShotBlock)}: {m_energyLossOnEnemyForShotBlock}, " +
                   $"{nameof(m_effectOnEnemyForShotBlock)}: {m_effectOnEnemyForShotBlock}, " +
                   $"{nameof(m_onEnemyCrossHitSequenceOverride)}: {m_onEnemyCrossHitSequenceOverride}, " +
                   $"{nameof(m_onAllyCrossHitSequenceOverride)}: {m_onAllyCrossHitSequenceOverride}, " +
                   $"{nameof(m_useBarrierSequenceOverride)}: {m_useBarrierSequenceOverride}, " +
                   $"{nameof(m_barrierSequenceOverrides)}: {m_barrierSequenceOverrides}, " +
                   $"{nameof(m_responseOnShotSequenceOverride)}: {m_responseOnShotSequenceOverride}, " +
                   $"{nameof(m_useShooterPosAsReactionSequenceTargetPosMod)}: {m_useShooterPosAsReactionSequenceTargetPosMod}, " +
                   ")";
        }
    }
}
