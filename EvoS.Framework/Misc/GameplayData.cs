using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("GameplayData")]
    public class GameplayData : MonoBehaviour
    {
        public GameplayData.DiagonalMovement m_diagonalMovement;
        public GameplayData.MovementMaximumType m_movementMaximumType;
        public GameplayData.AbilityRangeType m_abilityRangeType;
        public bool m_showTextForPowerUps;
        public float m_coverProtectionAngle;
        public float m_coverProtectionDmgMultiplier;
        public float m_coverMinDistance;
        public SerializedComponent m_coverShader;
        public int m_recentlySpawnedBonusMovement;
        public int m_recentlyRespawnedBonusMovement;
        public int m_recentlySpawnedDuration;
        public int m_recentlyRespawnedDuration;
        public float m_gravity;
        public int m_itemSlots;
        public int m_creditsPerTurn;
        public int m_startingCredits;
        public int m_creditsPerPlayerKill;
        public float m_creditBonusFractionPerExtraPlayer;
        public bool m_playerBountyCountsParticipation;
        public int m_creditsPerMinionKill;
        public bool m_minionBountyCountsParticipation;
        public bool m_participationlessBountiesGoToTeam;
        public int m_capturePointsPerTurn;
        public float m_distanceCanSeeIntoBrush;
        public int m_brushDisruptionTurns;
        public bool m_unsuppressInvisibilityOnEndOfPhase;
        public float m_proximityBasedInvisibilityMinDistance;
        public bool m_blindEnemyBreaksProximityBasedInvisibility;
        public bool m_recallAllowed;
        public bool m_recallOnlyWhenOutOfCombat;
        public float m_recallIncomingDamageMultiplier;
        public SerializedArray<int> m_turnsAbilitiesUnlock;
        public int m_turnCatalystsUnlock;
        public bool m_disableAbilitiesOnRespawn;
        public Ability.MovementAdjustment m_movementAllowedOnRespawn;
        public bool m_resolveDamageBetweenAbilityPhases;
        public bool m_resolveDamageAfterEvasion;
        public bool m_resolveDamageImmediatelyDuringMovement;
        public bool m_keepTechPointsOnRespawn;
        public bool m_enableItemGUI;
        public bool m_npcsShowTargeters;
        public bool m_showActorAbilityCooldowns;
        public float m_maximumPositionX;
        public float m_minimumPositionX;
        public float m_maximumPositionZ;
        public float m_minimumPositionZ;

        public GameplayData()
        {
        }

        public GameplayData(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_diagonalMovement = (GameplayData.DiagonalMovement) stream.ReadInt32();
            m_movementMaximumType = (GameplayData.MovementMaximumType) stream.ReadInt32();
            m_abilityRangeType = (GameplayData.AbilityRangeType) stream.ReadInt32();
            m_showTextForPowerUps = stream.ReadBoolean();
            stream.AlignTo();
            m_coverProtectionAngle = stream.ReadSingle();
            m_coverProtectionDmgMultiplier = stream.ReadSingle();
            m_coverMinDistance = stream.ReadSingle();
            m_coverShader = new SerializedComponent(assetFile, stream);
            m_recentlySpawnedBonusMovement = stream.ReadInt32();
            m_recentlyRespawnedBonusMovement = stream.ReadInt32();
            m_recentlySpawnedDuration = stream.ReadInt32();
            m_recentlyRespawnedDuration = stream.ReadInt32();
            m_gravity = stream.ReadSingle();
            m_itemSlots = stream.ReadInt32();
            m_creditsPerTurn = stream.ReadInt32();
            m_startingCredits = stream.ReadInt32();
            m_creditsPerPlayerKill = stream.ReadInt32();
            m_creditBonusFractionPerExtraPlayer = stream.ReadSingle();
            m_playerBountyCountsParticipation = stream.ReadBoolean();
            stream.AlignTo();
            m_creditsPerMinionKill = stream.ReadInt32();
            m_minionBountyCountsParticipation = stream.ReadBoolean();
            stream.AlignTo();
            m_participationlessBountiesGoToTeam = stream.ReadBoolean();
            stream.AlignTo();
            m_capturePointsPerTurn = stream.ReadInt32();
            m_distanceCanSeeIntoBrush = stream.ReadSingle();
            m_brushDisruptionTurns = stream.ReadInt32();
            m_unsuppressInvisibilityOnEndOfPhase = stream.ReadBoolean();
            stream.AlignTo();
            m_proximityBasedInvisibilityMinDistance = stream.ReadSingle();
            m_blindEnemyBreaksProximityBasedInvisibility = stream.ReadBoolean();
            stream.AlignTo();
            m_recallAllowed = stream.ReadBoolean();
            stream.AlignTo();
            m_recallOnlyWhenOutOfCombat = stream.ReadBoolean();
            stream.AlignTo();
            m_recallIncomingDamageMultiplier = stream.ReadSingle();
            m_turnsAbilitiesUnlock = new SerializedArray<int>(assetFile, stream);
            m_turnCatalystsUnlock = stream.ReadInt32();
            m_disableAbilitiesOnRespawn = stream.ReadBoolean();
            stream.AlignTo();
            m_movementAllowedOnRespawn = (Ability.MovementAdjustment) stream.ReadInt32();
            m_resolveDamageBetweenAbilityPhases = stream.ReadBoolean();
            stream.AlignTo();
            m_resolveDamageAfterEvasion = stream.ReadBoolean();
            stream.AlignTo();
            m_resolveDamageImmediatelyDuringMovement = stream.ReadBoolean();
            stream.AlignTo();
            m_keepTechPointsOnRespawn = stream.ReadBoolean();
            stream.AlignTo();
            m_enableItemGUI = stream.ReadBoolean();
            stream.AlignTo();
            m_npcsShowTargeters = stream.ReadBoolean();
            stream.AlignTo();
            m_showActorAbilityCooldowns = stream.ReadBoolean();
            stream.AlignTo();
            m_maximumPositionX = stream.ReadSingle();
            m_minimumPositionX = stream.ReadSingle();
            m_maximumPositionZ = stream.ReadSingle();
            m_minimumPositionZ = stream.ReadSingle();
        }

        public override string ToString()
        {
            return $"{nameof(GameplayData)}>(" +
                   $"{nameof(m_diagonalMovement)}: {m_diagonalMovement}, " +
                   $"{nameof(m_movementMaximumType)}: {m_movementMaximumType}, " +
                   $"{nameof(m_abilityRangeType)}: {m_abilityRangeType}, " +
                   $"{nameof(m_showTextForPowerUps)}: {m_showTextForPowerUps}, " +
                   $"{nameof(m_coverProtectionAngle)}: {m_coverProtectionAngle}, " +
                   $"{nameof(m_coverProtectionDmgMultiplier)}: {m_coverProtectionDmgMultiplier}, " +
                   $"{nameof(m_coverMinDistance)}: {m_coverMinDistance}, " +
                   $"{nameof(m_coverShader)}: {m_coverShader}, " +
                   $"{nameof(m_recentlySpawnedBonusMovement)}: {m_recentlySpawnedBonusMovement}, " +
                   $"{nameof(m_recentlyRespawnedBonusMovement)}: {m_recentlyRespawnedBonusMovement}, " +
                   $"{nameof(m_recentlySpawnedDuration)}: {m_recentlySpawnedDuration}, " +
                   $"{nameof(m_recentlyRespawnedDuration)}: {m_recentlyRespawnedDuration}, " +
                   $"{nameof(m_gravity)}: {m_gravity}, " +
                   $"{nameof(m_itemSlots)}: {m_itemSlots}, " +
                   $"{nameof(m_creditsPerTurn)}: {m_creditsPerTurn}, " +
                   $"{nameof(m_startingCredits)}: {m_startingCredits}, " +
                   $"{nameof(m_creditsPerPlayerKill)}: {m_creditsPerPlayerKill}, " +
                   $"{nameof(m_creditBonusFractionPerExtraPlayer)}: {m_creditBonusFractionPerExtraPlayer}, " +
                   $"{nameof(m_playerBountyCountsParticipation)}: {m_playerBountyCountsParticipation}, " +
                   $"{nameof(m_creditsPerMinionKill)}: {m_creditsPerMinionKill}, " +
                   $"{nameof(m_minionBountyCountsParticipation)}: {m_minionBountyCountsParticipation}, " +
                   $"{nameof(m_participationlessBountiesGoToTeam)}: {m_participationlessBountiesGoToTeam}, " +
                   $"{nameof(m_capturePointsPerTurn)}: {m_capturePointsPerTurn}, " +
                   $"{nameof(m_distanceCanSeeIntoBrush)}: {m_distanceCanSeeIntoBrush}, " +
                   $"{nameof(m_brushDisruptionTurns)}: {m_brushDisruptionTurns}, " +
                   $"{nameof(m_unsuppressInvisibilityOnEndOfPhase)}: {m_unsuppressInvisibilityOnEndOfPhase}, " +
                   $"{nameof(m_proximityBasedInvisibilityMinDistance)}: {m_proximityBasedInvisibilityMinDistance}, " +
                   $"{nameof(m_blindEnemyBreaksProximityBasedInvisibility)}: {m_blindEnemyBreaksProximityBasedInvisibility}, " +
                   $"{nameof(m_recallAllowed)}: {m_recallAllowed}, " +
                   $"{nameof(m_recallOnlyWhenOutOfCombat)}: {m_recallOnlyWhenOutOfCombat}, " +
                   $"{nameof(m_recallIncomingDamageMultiplier)}: {m_recallIncomingDamageMultiplier}, " +
                   $"{nameof(m_turnsAbilitiesUnlock)}: {m_turnsAbilitiesUnlock}, " +
                   $"{nameof(m_turnCatalystsUnlock)}: {m_turnCatalystsUnlock}, " +
                   $"{nameof(m_disableAbilitiesOnRespawn)}: {m_disableAbilitiesOnRespawn}, " +
                   $"{nameof(m_movementAllowedOnRespawn)}: {m_movementAllowedOnRespawn}, " +
                   $"{nameof(m_resolveDamageBetweenAbilityPhases)}: {m_resolveDamageBetweenAbilityPhases}, " +
                   $"{nameof(m_resolveDamageAfterEvasion)}: {m_resolveDamageAfterEvasion}, " +
                   $"{nameof(m_resolveDamageImmediatelyDuringMovement)}: {m_resolveDamageImmediatelyDuringMovement}, " +
                   $"{nameof(m_keepTechPointsOnRespawn)}: {m_keepTechPointsOnRespawn}, " +
                   $"{nameof(m_enableItemGUI)}: {m_enableItemGUI}, " +
                   $"{nameof(m_npcsShowTargeters)}: {m_npcsShowTargeters}, " +
                   $"{nameof(m_showActorAbilityCooldowns)}: {m_showActorAbilityCooldowns}, " +
                   $"{nameof(m_maximumPositionX)}: {m_maximumPositionX}, " +
                   $"{nameof(m_minimumPositionX)}: {m_minimumPositionX}, " +
                   $"{nameof(m_maximumPositionZ)}: {m_maximumPositionZ}, " +
                   $"{nameof(m_minimumPositionZ)}: {m_minimumPositionZ}, " +
                   ")";
        }

        public enum DiagonalMovement
        {
            Disabled,
            Enabled,
        }

        public enum MovementMaximumType
        {
            CannotExceedMax,
            StopAfterExceeding,
        }

        public enum AbilityRangeType
        {
            WorldDistToFreePos,
            BoardDistToBestSquare,
        }
    }
}
