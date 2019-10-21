using System;
using System.Collections.Generic;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.NetworkBehaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("GameplayMutators")]
    public class GameplayMutators : MonoBehaviour
    {
        public float m_energyGainMultiplier;
        public IntervalDef m_energyGainMultInterval;
        public float m_damageMultiplier;
        public IntervalDef m_damageMultInterval;
        public float m_healingMultiplier;
        public IntervalDef m_healingMultInterval;
        public float m_absorbMultiplier;
        public IntervalDef m_absorbMultInterval;
        public int m_cooldownSpeedAdjustment;
        public int m_cooldownTimeAdjustment;
        public float m_cooldownMultiplier;
        public int m_powerupRefreshSpeedAdjustment;
        public int m_powerupDurationAdjustment;
        public SerializedArray<StatusInterval> m_alwaysOnStatuses;
        public SerializedArray<StatusInterval> m_statusSuppression;
        public float m_passiveEnergyRegenMultiplier;
        public float m_passiveHpRegenMultiplier;
        public bool m_useEnergizedOverride;
        public AbilityModPropertyInt m_energizedEnergyGainMod;
        public bool m_useSlowEnergyGainOverride;
        public AbilityModPropertyInt m_slowEnergyGainEnergyGainMod;
        public bool m_useHasteOverride;
        public int m_hasteHalfMovementAdjustAmount;
        public int m_hasteFullMovementAdjustAmount;
        public float m_hasteMovementMultiplier;
        public bool m_useSlowOverride;
        public int m_slowHalfMovementAdjustAmount;
        public int m_slowFullMovementAdjustAmount;
        public float m_slowMovementMultiplier;
        public bool m_useEmpoweredOverride;
        public AbilityModPropertyInt m_empoweredOutgoingDamageMod;
        public AbilityModPropertyInt m_empoweredOutgoingHealingMod;
        public AbilityModPropertyInt m_empoweredOutgoingAbsorbMod;
        public bool m_useWeakenedOverride;
        public AbilityModPropertyInt m_weakenedOutgoingDamageMod;
        public AbilityModPropertyInt m_weakenedOutgoingHealingMod;
        public AbilityModPropertyInt m_weakenedOutgoingAbsorbMod;
        public bool m_useArmoredOverride;
        public AbilityModPropertyInt m_armoredIncomingDamageMod;
        public bool m_useVulnerableOverride;
        public float m_vulnerableDamageMultiplier;
        public int m_vulnerableDamageFlatAdd;

        public GameplayMutators()
        {
        }

        public GameplayMutators(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        private bool IsMultiplierActive(IntervalDef multInterval)
        {
            if (GameFlowData != null && multInterval != null)
                return IsActiveInCurrentActionPhase(GameFlowData.CurrentTurn,
                    multInterval.m_onDuration, multInterval.m_restDuration, multInterval.m_startOffset,
                    multInterval.m_delayTillStartOfMovement, ActionPhaseCheckMode.Default);
            return true;
        }

        public float GetEnergyGainMultiplier()
        {
            if (IsMultiplierActive(m_energyGainMultInterval))
                return m_energyGainMultiplier;
            return 1f;
        }

        public float GetDamageMultiplier()
        {
            if (IsMultiplierActive(m_damageMultInterval))
                return m_damageMultiplier;
            return 1f;
        }

        public float GetHealingMultiplier()
        {
            if (IsMultiplierActive(m_healingMultInterval))
                return m_healingMultiplier;
            return 1f;
        }

        public float GetAbsorbMultiplier()
        {
            if (IsMultiplierActive(m_absorbMultInterval))
                return m_absorbMultiplier;
            return 1f;
        }

        public int GetCooldownSpeedAdjustment()
        {
            return m_cooldownSpeedAdjustment;
        }

        public int GetCooldownTimeAdjustment()
        {
            return m_cooldownTimeAdjustment;
        }

        public float GetCooldownMultiplier()
        {
            return m_cooldownMultiplier;
        }

        public int GetPowerupRefreshSpeedAdjustment()
        {
            return m_powerupRefreshSpeedAdjustment;
        }

        public int GetPowerupDurationAdjustment()
        {
            return m_powerupDurationAdjustment;
        }

        public bool IsStatusActive(
            StatusType statusType,
            int currentTurn,
            ActionPhaseCheckMode phaseCheckMode = ActionPhaseCheckMode.Default)
        {
            if (m_alwaysOnStatuses == null) return false;
            foreach (var status in m_alwaysOnStatuses)
            {
                if (status.m_statusType != statusType) continue;
                int startOffset = status.m_startOffset;
                int duration = status.m_duration;
                int interval = status.m_interval;
                bool tillStartOfMovement = status.m_delayTillStartOfMovement;
                return IsActiveInCurrentActionPhase(currentTurn, duration, interval, startOffset, tillStartOfMovement,
                    phaseCheckMode);
            }

            return false;
        }

        public bool IsStatusSuppressed(
            StatusType statusType,
            int currentTurn,
            ActionPhaseCheckMode phaseCheckMode = ActionPhaseCheckMode.Default)
        {
            if (m_statusSuppression == null) return false;
            var statusSuppression = m_statusSuppression;
            for (var index = 0; index < statusSuppression.Count; ++index)
            {
                if (statusSuppression[index].m_statusType != statusType) continue;
                var statusInterval = statusSuppression[index];
                var startOffset = statusInterval.m_startOffset;
                var duration = statusInterval.m_duration;
                var interval = statusInterval.m_interval;
                var tillStartOfMovement = statusInterval.m_delayTillStartOfMovement;
                return IsActiveInCurrentActionPhase(currentTurn, duration, interval,
                    startOffset, tillStartOfMovement, phaseCheckMode);
            }

            return false;
        }

        public bool IsActiveInCurrentActionPhase(
            int currentTurn,
            int onDuration,
            int restDuration,
            int startOffset,
            bool delayTillMoveStart,
            ActionPhaseCheckMode phaseCheckMode)
        {
            bool flag1 = false;
            bool flag2;
            if ((flag2 = IsActiveInCurrentTurn(currentTurn, onDuration, restDuration, startOffset)) &&
                phaseCheckMode == ActionPhaseCheckMode.Any)
                flag1 = true;
            else if (flag2)
            {
                if (delayTillMoveStart)
                {
                    bool flag3 = IsActiveInCurrentTurn(currentTurn - 1, onDuration, restDuration, startOffset);
                    if (!IsOnStartOfIntervalCycle(currentTurn, onDuration, restDuration,
                            startOffset) && flag3)
                        flag1 = true;
                    else if (!(flag1 = phaseCheckMode == ActionPhaseCheckMode.Movement) &&
                             phaseCheckMode == ActionPhaseCheckMode.Default)
                    {
                        switch (ServerClientUtils.GetCurrentActionPhase())
                        {
                            case ActionBufferPhase.AbilitiesWait:
                            case ActionBufferPhase.Movement:
                            case ActionBufferPhase.MovementChase:
                            case ActionBufferPhase.MovementWait:
                                flag1 = true;
                                break;
                            case ActionBufferPhase.Done:
                                if (GameFlowData.gameState == GameState.BothTeams_Resolve ||
                                    GameFlowData.gameState == GameState.EndingTurn)
                                    goto case ActionBufferPhase.AbilitiesWait;
                                else
                                    break;
                        }
                    }
                }
                else
                    flag1 = true;
            }

            return flag1;
        }

        private bool IsActiveInCurrentTurn(
            int currentTurn,
            int onDuration,
            int restDuration,
            int startOffset)
        {
            if (startOffset > 0 && currentTurn <= startOffset)
                return false;
            if (onDuration <= 0)
                return true;
            int num1 = currentTurn;
            if (startOffset > 0)
                num1 -= startOffset;
            int num2 = restDuration + onDuration;
            return (num1 - 1) % num2 < onDuration;
        }

        public bool IsOnStartOfIntervalCycle(
            int currentTurn,
            int onDuration,
            int restDuration,
            int startOffset)
        {
            if (startOffset > 0 && currentTurn <= startOffset || onDuration <= 0)
                return false;
            int num1 = currentTurn;
            if (startOffset > 0)
                num1 -= startOffset;
            int num2 = restDuration + onDuration;
            return (num1 - 1) % num2 == 0;
        }

        public float GetPassiveEnergyRegenMultiplier()
        {
            return m_passiveEnergyRegenMultiplier;
        }

        public float GetPassiveHpRegenMultiplier()
        {
            return m_passiveHpRegenMultiplier;
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_energyGainMultiplier = stream.ReadSingle();
            m_energyGainMultInterval = new IntervalDef(assetFile, stream);
            m_damageMultiplier = stream.ReadSingle();
            m_damageMultInterval = new IntervalDef(assetFile, stream);
            m_healingMultiplier = stream.ReadSingle();
            m_healingMultInterval = new IntervalDef(assetFile, stream);
            m_absorbMultiplier = stream.ReadSingle();
            m_absorbMultInterval = new IntervalDef(assetFile, stream);
            m_cooldownSpeedAdjustment = stream.ReadInt32();
            m_cooldownTimeAdjustment = stream.ReadInt32();
            m_cooldownMultiplier = stream.ReadSingle();
            m_powerupRefreshSpeedAdjustment = stream.ReadInt32();
            m_powerupDurationAdjustment = stream.ReadInt32();
            m_alwaysOnStatuses = new SerializedArray<StatusInterval>(assetFile, stream);
            m_statusSuppression = new SerializedArray<StatusInterval>(assetFile, stream);
            m_passiveEnergyRegenMultiplier = stream.ReadSingle();
            m_passiveHpRegenMultiplier = stream.ReadSingle();
            m_useEnergizedOverride = stream.ReadBoolean();
            stream.AlignTo();
            m_energizedEnergyGainMod = new AbilityModPropertyInt(assetFile, stream);
            m_useSlowEnergyGainOverride = stream.ReadBoolean();
            stream.AlignTo();
            m_slowEnergyGainEnergyGainMod = new AbilityModPropertyInt(assetFile, stream);
            m_useHasteOverride = stream.ReadBoolean();
            stream.AlignTo();
            m_hasteHalfMovementAdjustAmount = stream.ReadInt32();
            m_hasteFullMovementAdjustAmount = stream.ReadInt32();
            m_hasteMovementMultiplier = stream.ReadSingle();
            m_useSlowOverride = stream.ReadBoolean();
            stream.AlignTo();
            m_slowHalfMovementAdjustAmount = stream.ReadInt32();
            m_slowFullMovementAdjustAmount = stream.ReadInt32();
            m_slowMovementMultiplier = stream.ReadSingle();
            m_useEmpoweredOverride = stream.ReadBoolean();
            stream.AlignTo();
            m_empoweredOutgoingDamageMod = new AbilityModPropertyInt(assetFile, stream);
            m_empoweredOutgoingHealingMod = new AbilityModPropertyInt(assetFile, stream);
            m_empoweredOutgoingAbsorbMod = new AbilityModPropertyInt(assetFile, stream);
            m_useWeakenedOverride = stream.ReadBoolean();
            stream.AlignTo();
            m_weakenedOutgoingDamageMod = new AbilityModPropertyInt(assetFile, stream);
            m_weakenedOutgoingHealingMod = new AbilityModPropertyInt(assetFile, stream);
            m_weakenedOutgoingAbsorbMod = new AbilityModPropertyInt(assetFile, stream);
            m_useArmoredOverride = stream.ReadBoolean();
            stream.AlignTo();
            m_armoredIncomingDamageMod = new AbilityModPropertyInt(assetFile, stream);
            m_useVulnerableOverride = stream.ReadBoolean();
            stream.AlignTo();
            m_vulnerableDamageMultiplier = stream.ReadSingle();
            m_vulnerableDamageFlatAdd = stream.ReadInt32();
        }

        public override string ToString()
        {
            return $"{nameof(GameplayMutators)}(" +
                   $"{nameof(m_energyGainMultiplier)}: {m_energyGainMultiplier}, " +
                   $"{nameof(m_energyGainMultInterval)}: {m_energyGainMultInterval}, " +
                   $"{nameof(m_damageMultiplier)}: {m_damageMultiplier}, " +
                   $"{nameof(m_damageMultInterval)}: {m_damageMultInterval}, " +
                   $"{nameof(m_healingMultiplier)}: {m_healingMultiplier}, " +
                   $"{nameof(m_healingMultInterval)}: {m_healingMultInterval}, " +
                   $"{nameof(m_absorbMultiplier)}: {m_absorbMultiplier}, " +
                   $"{nameof(m_absorbMultInterval)}: {m_absorbMultInterval}, " +
                   $"{nameof(m_cooldownSpeedAdjustment)}: {m_cooldownSpeedAdjustment}, " +
                   $"{nameof(m_cooldownTimeAdjustment)}: {m_cooldownTimeAdjustment}, " +
                   $"{nameof(m_cooldownMultiplier)}: {m_cooldownMultiplier}, " +
                   $"{nameof(m_powerupRefreshSpeedAdjustment)}: {m_powerupRefreshSpeedAdjustment}, " +
                   $"{nameof(m_powerupDurationAdjustment)}: {m_powerupDurationAdjustment}, " +
                   $"{nameof(m_alwaysOnStatuses)}: {m_alwaysOnStatuses}, " +
                   $"{nameof(m_statusSuppression)}: {m_statusSuppression}, " +
                   $"{nameof(m_passiveEnergyRegenMultiplier)}: {m_passiveEnergyRegenMultiplier}, " +
                   $"{nameof(m_passiveHpRegenMultiplier)}: {m_passiveHpRegenMultiplier}, " +
                   $"{nameof(m_useEnergizedOverride)}: {m_useEnergizedOverride}, " +
                   $"{nameof(m_energizedEnergyGainMod)}: {m_energizedEnergyGainMod}, " +
                   $"{nameof(m_useSlowEnergyGainOverride)}: {m_useSlowEnergyGainOverride}, " +
                   $"{nameof(m_slowEnergyGainEnergyGainMod)}: {m_slowEnergyGainEnergyGainMod}, " +
                   $"{nameof(m_useHasteOverride)}: {m_useHasteOverride}, " +
                   $"{nameof(m_hasteHalfMovementAdjustAmount)}: {m_hasteHalfMovementAdjustAmount}, " +
                   $"{nameof(m_hasteFullMovementAdjustAmount)}: {m_hasteFullMovementAdjustAmount}, " +
                   $"{nameof(m_hasteMovementMultiplier)}: {m_hasteMovementMultiplier}, " +
                   $"{nameof(m_useSlowOverride)}: {m_useSlowOverride}, " +
                   $"{nameof(m_slowHalfMovementAdjustAmount)}: {m_slowHalfMovementAdjustAmount}, " +
                   $"{nameof(m_slowFullMovementAdjustAmount)}: {m_slowFullMovementAdjustAmount}, " +
                   $"{nameof(m_slowMovementMultiplier)}: {m_slowMovementMultiplier}, " +
                   $"{nameof(m_useEmpoweredOverride)}: {m_useEmpoweredOverride}, " +
                   $"{nameof(m_empoweredOutgoingDamageMod)}: {m_empoweredOutgoingDamageMod}, " +
                   $"{nameof(m_empoweredOutgoingHealingMod)}: {m_empoweredOutgoingHealingMod}, " +
                   $"{nameof(m_empoweredOutgoingAbsorbMod)}: {m_empoweredOutgoingAbsorbMod}, " +
                   $"{nameof(m_useWeakenedOverride)}: {m_useWeakenedOverride}, " +
                   $"{nameof(m_weakenedOutgoingDamageMod)}: {m_weakenedOutgoingDamageMod}, " +
                   $"{nameof(m_weakenedOutgoingHealingMod)}: {m_weakenedOutgoingHealingMod}, " +
                   $"{nameof(m_weakenedOutgoingAbsorbMod)}: {m_weakenedOutgoingAbsorbMod}, " +
                   $"{nameof(m_useArmoredOverride)}: {m_useArmoredOverride}, " +
                   $"{nameof(m_armoredIncomingDamageMod)}: {m_armoredIncomingDamageMod}, " +
                   $"{nameof(m_useVulnerableOverride)}: {m_useVulnerableOverride}, " +
                   $"{nameof(m_vulnerableDamageMultiplier)}: {m_vulnerableDamageMultiplier}, " +
                   $"{nameof(m_vulnerableDamageFlatAdd)}: {m_vulnerableDamageFlatAdd}, " +
                   ")";
        }

        [Serializable]
        public struct StatusInterval
        {
            public StatusType m_statusType;
            public int m_duration;
            public int m_interval;
            public int m_startOffset;
            public bool m_delayTillStartOfMovement;
            public string m_activateNotificationTurnBefore;
            public string m_offNotificationTurnBefore;
        }

        [Serializable]
        public class IntervalDef
        {
            public int m_onDuration;
            public int m_restDuration;
            public int m_startOffset;
            public bool m_delayTillStartOfMovement;

            public IntervalDef(AssetFile assetFile, StreamReader stream)
            {
                m_onDuration = stream.ReadInt32();
                m_restDuration = stream.ReadInt32();
                m_startOffset = stream.ReadInt32();
                m_delayTillStartOfMovement = stream.ReadBoolean();
                stream.AlignTo();
            }
        }

        public enum ActionPhaseCheckMode
        {
            Default,
            Abilities,
            Movement,
            Any
        }
    }
}
