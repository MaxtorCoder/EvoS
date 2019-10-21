using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("SinglePlayerState")]
    public class SinglePlayerState : ISerializedItem
    {
        public string m_stateTitle;
        public SerializedArray<int> m_allowedAbilities;
        public BoardRegion m_allowedDestinations;
        public BoardRegion m_advanceScriptDestinations;
        public BoardRegion m_respawnDestinations;
        public BoardRegion m_allowedTargets;
        public bool m_mustTargetNearCenter;
        public bool m_onlyAllowWaypointMovement;
        public BoardRegion m_rightClickHighlight;
        public string m_rightClickText;
        public float m_rightClickHeight;
        public BoardRegion m_shiftRightClickHighlight;
        public string m_shiftRightClickText;
        public float m_shiftRightClickHeight;
        public BoardRegion m_leftClickHighlight;
        public string m_leftClickText;
        public float m_leftClickHeight;
        public int m_minAbilityTargetsForAiming;
        public string m_bannerText;
        public TextRegion m_tutorialBoxText;
        public TextRegion m_tutorialBoxText2;
        public TextRegion m_tutorialBoxText3;
        public TextRegion m_tutorialCameraMovementText;
        public TextRegion m_tutorialCameraRotationText;
        public string m_errorStringOnForbiddenPath;
        public SerializedComponent m_cameraRotationTarget;
        public float m_advanceAfterSeconds;
        public int m_minPlayerHitPoints;
        public int m_minAllyHitPoints;
        public string m_audioEventOnPreEnter;
        public string m_audioEventOnEnter;
        public string m_audioEventOnExit;
        public SerializedArray<SinglePlayerScriptedChat> m_chatTextOnEnter;
        public string m_tutorialVideoPreviewOnEnter;
        public bool m_markedForAdvanceState;
        public int m_stateIndex;
        public float m_startTime;
        public SerializedArray<SinglePlayerTag> m_tags;
        public SerializedArray<ActivatableObject> m_activationsOnEnter;
        public SerializedArray<ActivatableObject> m_activationsOnExit;
        public SerializedArray<ActivatableUI> m_uiActivationsOnEnter;
        public SerializedArray<ActivatableUI> m_uiActivationsOnExit;
        public SerializedArray<string> m_npcsToSpawnOnEnter;
        public SerializedArray<string> m_npcsToDespawnOnEnter;
        public ActorDiesTrigger m_advanceScriptIfActorDies;
        public int m_advanceScriptIfActorDiesCount;
        public int m_actorDeaths;

        public SinglePlayerState()
        {
        }

        public SinglePlayerState(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_stateTitle = stream.ReadString32();
            m_allowedAbilities = new SerializedArray<int>(assetFile, stream);
            m_allowedDestinations = new BoardRegion(assetFile, stream);
            m_advanceScriptDestinations = new BoardRegion(assetFile, stream);
            m_respawnDestinations = new BoardRegion(assetFile, stream);
            m_allowedTargets = new BoardRegion(assetFile, stream);
            m_mustTargetNearCenter = stream.ReadBoolean();
            stream.AlignTo();
            m_onlyAllowWaypointMovement = stream.ReadBoolean();
            stream.AlignTo();
            m_rightClickHighlight = new BoardRegion(assetFile, stream);
            m_rightClickText = stream.ReadString32();
            m_rightClickHeight = stream.ReadSingle();
            m_shiftRightClickHighlight = new BoardRegion(assetFile, stream);
            m_shiftRightClickText = stream.ReadString32();
            m_shiftRightClickHeight = stream.ReadSingle();
            m_leftClickHighlight = new BoardRegion(assetFile, stream);
            m_leftClickText = stream.ReadString32();
            m_leftClickHeight = stream.ReadSingle();
            m_minAbilityTargetsForAiming = stream.ReadInt32();
            m_bannerText = stream.ReadString32();
            m_tutorialBoxText = new TextRegion(assetFile, stream);
            m_tutorialBoxText2 = new TextRegion(assetFile, stream);
            m_tutorialBoxText3 = new TextRegion(assetFile, stream);
            m_tutorialCameraMovementText = new TextRegion(assetFile, stream);
            m_tutorialCameraRotationText = new TextRegion(assetFile, stream);
            m_errorStringOnForbiddenPath = stream.ReadString32();
            m_cameraRotationTarget = new SerializedComponent(assetFile, stream);
            m_advanceAfterSeconds = stream.ReadSingle();
            m_minPlayerHitPoints = stream.ReadInt32();
            m_minAllyHitPoints = stream.ReadInt32();
            m_audioEventOnPreEnter = stream.ReadString32();
            m_audioEventOnEnter = stream.ReadString32();
            m_audioEventOnExit = stream.ReadString32();
            m_chatTextOnEnter = new SerializedArray<SinglePlayerScriptedChat>(assetFile, stream);
            m_tutorialVideoPreviewOnEnter = stream.ReadString32();
            m_markedForAdvanceState = stream.ReadBoolean();
            stream.AlignTo();
            m_stateIndex = stream.ReadInt32();
            m_startTime = stream.ReadSingle();
            m_tags = new SerializedArray<SinglePlayerTag>(assetFile, stream);
            m_activationsOnEnter = new SerializedArray<ActivatableObject>(assetFile, stream);
            m_activationsOnExit = new SerializedArray<ActivatableObject>(assetFile, stream);
            m_uiActivationsOnEnter = new SerializedArray<ActivatableUI>(assetFile, stream);
            m_uiActivationsOnExit = new SerializedArray<ActivatableUI>(assetFile, stream);
            m_npcsToSpawnOnEnter = new SerializedArray<string>(assetFile, stream);
            m_npcsToDespawnOnEnter = new SerializedArray<string>(assetFile, stream);
            m_advanceScriptIfActorDies = (ActorDiesTrigger) stream.ReadInt32();
            m_advanceScriptIfActorDiesCount = stream.ReadInt32();
            m_actorDeaths = stream.ReadInt32();
        }

        public override string ToString()
        {
            return $"{nameof(SinglePlayerState)}>(" +
                   $"{nameof(m_stateTitle)}: {m_stateTitle}, " +
                   $"{nameof(m_allowedAbilities)}: {m_allowedAbilities}, " +
                   $"{nameof(m_allowedDestinations)}: {m_allowedDestinations}, " +
                   $"{nameof(m_advanceScriptDestinations)}: {m_advanceScriptDestinations}, " +
                   $"{nameof(m_respawnDestinations)}: {m_respawnDestinations}, " +
                   $"{nameof(m_allowedTargets)}: {m_allowedTargets}, " +
                   $"{nameof(m_mustTargetNearCenter)}: {m_mustTargetNearCenter}, " +
                   $"{nameof(m_onlyAllowWaypointMovement)}: {m_onlyAllowWaypointMovement}, " +
                   $"{nameof(m_rightClickHighlight)}: {m_rightClickHighlight}, " +
                   $"{nameof(m_rightClickText)}: {m_rightClickText}, " +
                   $"{nameof(m_rightClickHeight)}: {m_rightClickHeight}, " +
                   $"{nameof(m_shiftRightClickHighlight)}: {m_shiftRightClickHighlight}, " +
                   $"{nameof(m_shiftRightClickText)}: {m_shiftRightClickText}, " +
                   $"{nameof(m_shiftRightClickHeight)}: {m_shiftRightClickHeight}, " +
                   $"{nameof(m_leftClickHighlight)}: {m_leftClickHighlight}, " +
                   $"{nameof(m_leftClickText)}: {m_leftClickText}, " +
                   $"{nameof(m_leftClickHeight)}: {m_leftClickHeight}, " +
                   $"{nameof(m_minAbilityTargetsForAiming)}: {m_minAbilityTargetsForAiming}, " +
                   $"{nameof(m_bannerText)}: {m_bannerText}, " +
                   $"{nameof(m_tutorialBoxText)}: {m_tutorialBoxText}, " +
                   $"{nameof(m_tutorialBoxText2)}: {m_tutorialBoxText2}, " +
                   $"{nameof(m_tutorialBoxText3)}: {m_tutorialBoxText3}, " +
                   $"{nameof(m_tutorialCameraMovementText)}: {m_tutorialCameraMovementText}, " +
                   $"{nameof(m_tutorialCameraRotationText)}: {m_tutorialCameraRotationText}, " +
                   $"{nameof(m_errorStringOnForbiddenPath)}: {m_errorStringOnForbiddenPath}, " +
                   $"{nameof(m_cameraRotationTarget)}: {m_cameraRotationTarget}, " +
                   $"{nameof(m_advanceAfterSeconds)}: {m_advanceAfterSeconds}, " +
                   $"{nameof(m_minPlayerHitPoints)}: {m_minPlayerHitPoints}, " +
                   $"{nameof(m_minAllyHitPoints)}: {m_minAllyHitPoints}, " +
                   $"{nameof(m_audioEventOnPreEnter)}: {m_audioEventOnPreEnter}, " +
                   $"{nameof(m_audioEventOnEnter)}: {m_audioEventOnEnter}, " +
                   $"{nameof(m_audioEventOnExit)}: {m_audioEventOnExit}, " +
                   $"{nameof(m_chatTextOnEnter)}: {m_chatTextOnEnter}, " +
                   $"{nameof(m_tutorialVideoPreviewOnEnter)}: {m_tutorialVideoPreviewOnEnter}, " +
                   $"{nameof(m_markedForAdvanceState)}: {m_markedForAdvanceState}, " +
                   $"{nameof(m_stateIndex)}: {m_stateIndex}, " +
                   $"{nameof(m_startTime)}: {m_startTime}, " +
                   $"{nameof(m_tags)}: {m_tags}, " +
                   $"{nameof(m_activationsOnEnter)}: {m_activationsOnEnter}, " +
                   $"{nameof(m_activationsOnExit)}: {m_activationsOnExit}, " +
                   $"{nameof(m_uiActivationsOnEnter)}: {m_uiActivationsOnEnter}, " +
                   $"{nameof(m_uiActivationsOnExit)}: {m_uiActivationsOnExit}, " +
                   $"{nameof(m_npcsToSpawnOnEnter)}: {m_npcsToSpawnOnEnter}, " +
                   $"{nameof(m_npcsToDespawnOnEnter)}: {m_npcsToDespawnOnEnter}, " +
                   $"{nameof(m_advanceScriptIfActorDies)}: {m_advanceScriptIfActorDies}, " +
                   $"{nameof(m_advanceScriptIfActorDiesCount)}: {m_advanceScriptIfActorDiesCount}, " +
                   $"{nameof(m_actorDeaths)}: {m_actorDeaths}, " +
                   ")";
        }

        public enum SinglePlayerTag
        {
            PauseTimer,
            RequireShootAndMove,
            DefaultCinematicCam,
            ForceOnCinematicCam,
            ShutdownOnExit,
            AdvanceOnNewTurn,
            RequireMaxPossibleAbilities,
            RequireMoveToAdvanceScriptDestination,
            ClearCooldownsOnEnter,
            DEPRECATED1,
            AdvanceOnMoveEntered,
            AdvanceOnAbilitySelected,
            AdvanceOnAbilityTargeted,
            AdvanceOnLockInEntered,
            DisableCancel,
            RequireChasing,
            RequireDash,
            TeleportToAdvanceScriptDestination,
            ResetHealthOnEnter,
            ResetEnergyOnEnter,
            EnableChatter,
            EnableAutoQueuedAbilitiesForNpcs,
            EnableHiddenMovementText,
            EnableBrush,
            DisableAdvanceTurn,
            AdvanceOnDecisionEnd,
            AdvanceOnResolutionEnd,
            EnableCooldownIndicators,
            ResetContributionOnEnter,
            AdvanceOnTutorialQueueEmpty
        }

        public enum ActorDiesTrigger
        {
            Never,
            AnyActor,
            SpawnedNPCs,
            Players,
            ClientActor,
            ClientAlly,
            ClientEnemy
        }
    }
}
