using System;
using System.Drawing;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Misc;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("ControlPoint")]
    public class ControlPoint : NetworkBehaviour
    {
        public string m_displayName;
        public BoardRegion m_region;
        public State m_startingState;
        public ControlProgressType m_progressType;
        public bool m_stayControlledUntilOtherTeamCaptures;
        public bool m_resetProgressOnceCaptured;
        public bool m_resetProgressOnceDisabled;
        public int m_progressNeededForTeamAToCapture;
        public int m_maxTotalProgressForTeamA;
        public int m_maxProgressForTeamAOnceControlled;
        public int m_progressNeededForTeamBToCapture;
        public int m_maxTotalProgressForTeamB;
        public int m_maxProgressForTeamBOnceControlled;
        public int m_maxProgressChangeInOneTurn;
        public int m_progressDecayPerTurn;
        public int m_numVacantTurnsUntilProgressDecays;
        public bool m_allowIndependentVacancyDecay;
        public int m_turnsLockedAfterCapture;
        public int m_turnsLockedAfterActivated;
        public bool m_canContributeProgressWhileContested;
        public int m_startingProgress;
        public ControlPointGameplay m_controllingTeamGameplay;
        public ControlPointGameplay m_otherTeamGameplay;
        public int m_totalObjectivePointsToDispense;
        public bool m_disableWhenDispensedLastObjectivePoint;
        public SerializedVector<SerializedGameObject> m_controlPointsToActivateOnDisabled;
        public int m_numRandomControlPointsToActivate;
        public int m_randomActivateTurnsLockedOverride;
        public bool m_randomActivateIgnoreIfEverActivated;
        public VisionGranting m_visionGranting;
        public bool m_visionSeeThroughBrush;
        public BoardRegion m_visionRegionOverride;
        public VisionGranting m_whenToApplyHealing;
        public int m_healPerTurn;
        public SerializedComponent m_healHitSequencePrefab;
        public bool m_spawnForMatchingTeams;
        public SerializedArray<SerializedGameObject> m_spawnersForController;
        public bool m_autoGenerateBoundaryVisuals;
        public float m_boundaryOscillationSpeed;
        public float m_boundaryOscillationHeight;
        public SerializedComponent m_nameplateOverridePosition;
        public SerializedComponent m_boundaryNeutral;
        public SerializedComponent m_boundaryAllied;
        public SerializedComponent m_boundaryEnemy;
        public SerializedComponent m_boundaryDisabled;
        public ColorRGBA m_primaryColor_friendly;
        public ColorRGBA m_primaryColor_hostile;
        public ColorRGBA m_primaryColor_neutral;
        public ColorRGBA m_secondaryColor_contested;
        public ColorRGBA m_secondaryColor_friendlyCapturing;
        public ColorRGBA m_secondaryColor_hostileCapturing;
        public ColorRGBA m_uiTextColor_Empty;
        public ColorRGBA m_uiTextColor_Locked;
        public SerializedComponent m_icon;
        public ColorRGBA m_miniMapColorNeutral;
        public ColorRGBA m_miniMapColorAllied;
        public ColorRGBA m_miniMapColorEnemy;
        public ColorRGBA m_miniMapColorDisabled;
        public SerializedComponent m_miniMapImage;
        public ColorRGBA m_currentMinimapColor;
        public SerializedArray<CaptureMessage> m_captureMessages;

        public ControlPoint()
        {
        }

        public ControlPoint(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_displayName = stream.ReadString32();
            m_region = new BoardRegion(assetFile, stream);
            m_startingState = (State) stream.ReadInt32();
            m_progressType = (ControlProgressType) stream.ReadInt32();
            m_stayControlledUntilOtherTeamCaptures = stream.ReadBoolean();
            stream.AlignTo();
            m_resetProgressOnceCaptured = stream.ReadBoolean();
            stream.AlignTo();
            m_resetProgressOnceDisabled = stream.ReadBoolean();
            stream.AlignTo();
            m_progressNeededForTeamAToCapture = stream.ReadInt32();
            m_maxTotalProgressForTeamA = stream.ReadInt32();
            m_maxProgressForTeamAOnceControlled = stream.ReadInt32();
            m_progressNeededForTeamBToCapture = stream.ReadInt32();
            m_maxTotalProgressForTeamB = stream.ReadInt32();
            m_maxProgressForTeamBOnceControlled = stream.ReadInt32();
            m_maxProgressChangeInOneTurn = stream.ReadInt32();
            m_progressDecayPerTurn = stream.ReadInt32();
            m_numVacantTurnsUntilProgressDecays = stream.ReadInt32();
            m_allowIndependentVacancyDecay = stream.ReadBoolean();
            stream.AlignTo();
            m_turnsLockedAfterCapture = stream.ReadInt32();
            m_turnsLockedAfterActivated = stream.ReadInt32();
            m_canContributeProgressWhileContested = stream.ReadBoolean();
            stream.AlignTo();
            m_startingProgress = stream.ReadInt32();
            m_controllingTeamGameplay = new ControlPointGameplay(assetFile, stream);
            m_otherTeamGameplay = new ControlPointGameplay(assetFile, stream);
            m_totalObjectivePointsToDispense = stream.ReadInt32();
            m_disableWhenDispensedLastObjectivePoint = stream.ReadBoolean();
            stream.AlignTo();
            m_controlPointsToActivateOnDisabled = new SerializedVector<SerializedGameObject>(assetFile, stream);
            m_numRandomControlPointsToActivate = stream.ReadInt32();
            m_randomActivateTurnsLockedOverride = stream.ReadInt32();
            m_randomActivateIgnoreIfEverActivated = stream.ReadBoolean();
            stream.AlignTo();
            m_visionGranting = (VisionGranting) stream.ReadInt32();
            m_visionSeeThroughBrush = stream.ReadBoolean();
            stream.AlignTo();
            m_visionRegionOverride = new BoardRegion(assetFile, stream);
            m_whenToApplyHealing = (VisionGranting) stream.ReadInt32();
            m_healPerTurn = stream.ReadInt32();
            m_healHitSequencePrefab = new SerializedComponent(assetFile, stream);
            m_spawnForMatchingTeams = stream.ReadBoolean();
            stream.AlignTo();
            m_spawnersForController = new SerializedArray<SerializedGameObject>(assetFile, stream);
            m_autoGenerateBoundaryVisuals = stream.ReadBoolean();
            stream.AlignTo();
            m_boundaryOscillationSpeed = stream.ReadSingle();
            m_boundaryOscillationHeight = stream.ReadSingle();
            m_nameplateOverridePosition = new SerializedComponent(assetFile, stream);
            m_boundaryNeutral = new SerializedComponent(assetFile, stream);
            m_boundaryAllied = new SerializedComponent(assetFile, stream);
            m_boundaryEnemy = new SerializedComponent(assetFile, stream);
            m_boundaryDisabled = new SerializedComponent(assetFile, stream);
            m_primaryColor_friendly = stream.ReadColorRGBA();
            m_primaryColor_hostile = stream.ReadColorRGBA();
            m_primaryColor_neutral = stream.ReadColorRGBA();
            m_secondaryColor_contested = stream.ReadColorRGBA();
            m_secondaryColor_friendlyCapturing = stream.ReadColorRGBA();
            m_secondaryColor_hostileCapturing = stream.ReadColorRGBA();
            m_uiTextColor_Empty = stream.ReadColorRGBA();
            m_uiTextColor_Locked = stream.ReadColorRGBA();
            m_icon = new SerializedComponent(assetFile, stream);
            m_miniMapColorNeutral = stream.ReadColorRGBA();
            m_miniMapColorAllied = stream.ReadColorRGBA();
            m_miniMapColorEnemy = stream.ReadColorRGBA();
            m_miniMapColorDisabled = stream.ReadColorRGBA();
            m_miniMapImage = new SerializedComponent(assetFile, stream);
            m_currentMinimapColor = stream.ReadColorRGBA();
            m_captureMessages = new SerializedArray<CaptureMessage>(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(ControlPoint)}(" +
                   $"{nameof(m_displayName)}: {m_displayName}, " +
                   $"{nameof(m_region)}: {m_region}, " +
                   $"{nameof(m_startingState)}: {m_startingState}, " +
                   $"{nameof(m_progressType)}: {m_progressType}, " +
                   $"{nameof(m_stayControlledUntilOtherTeamCaptures)}: {m_stayControlledUntilOtherTeamCaptures}, " +
                   $"{nameof(m_resetProgressOnceCaptured)}: {m_resetProgressOnceCaptured}, " +
                   $"{nameof(m_resetProgressOnceDisabled)}: {m_resetProgressOnceDisabled}, " +
                   $"{nameof(m_progressNeededForTeamAToCapture)}: {m_progressNeededForTeamAToCapture}, " +
                   $"{nameof(m_maxTotalProgressForTeamA)}: {m_maxTotalProgressForTeamA}, " +
                   $"{nameof(m_maxProgressForTeamAOnceControlled)}: {m_maxProgressForTeamAOnceControlled}, " +
                   $"{nameof(m_progressNeededForTeamBToCapture)}: {m_progressNeededForTeamBToCapture}, " +
                   $"{nameof(m_maxTotalProgressForTeamB)}: {m_maxTotalProgressForTeamB}, " +
                   $"{nameof(m_maxProgressForTeamBOnceControlled)}: {m_maxProgressForTeamBOnceControlled}, " +
                   $"{nameof(m_maxProgressChangeInOneTurn)}: {m_maxProgressChangeInOneTurn}, " +
                   $"{nameof(m_progressDecayPerTurn)}: {m_progressDecayPerTurn}, " +
                   $"{nameof(m_numVacantTurnsUntilProgressDecays)}: {m_numVacantTurnsUntilProgressDecays}, " +
                   $"{nameof(m_allowIndependentVacancyDecay)}: {m_allowIndependentVacancyDecay}, " +
                   $"{nameof(m_turnsLockedAfterCapture)}: {m_turnsLockedAfterCapture}, " +
                   $"{nameof(m_turnsLockedAfterActivated)}: {m_turnsLockedAfterActivated}, " +
                   $"{nameof(m_canContributeProgressWhileContested)}: {m_canContributeProgressWhileContested}, " +
                   $"{nameof(m_startingProgress)}: {m_startingProgress}, " +
                   $"{nameof(m_controllingTeamGameplay)}: {m_controllingTeamGameplay}, " +
                   $"{nameof(m_otherTeamGameplay)}: {m_otherTeamGameplay}, " +
                   $"{nameof(m_totalObjectivePointsToDispense)}: {m_totalObjectivePointsToDispense}, " +
                   $"{nameof(m_disableWhenDispensedLastObjectivePoint)}: {m_disableWhenDispensedLastObjectivePoint}, " +
                   $"{nameof(m_controlPointsToActivateOnDisabled)}: {m_controlPointsToActivateOnDisabled}, " +
                   $"{nameof(m_numRandomControlPointsToActivate)}: {m_numRandomControlPointsToActivate}, " +
                   $"{nameof(m_randomActivateTurnsLockedOverride)}: {m_randomActivateTurnsLockedOverride}, " +
                   $"{nameof(m_randomActivateIgnoreIfEverActivated)}: {m_randomActivateIgnoreIfEverActivated}, " +
                   $"{nameof(m_visionGranting)}: {m_visionGranting}, " +
                   $"{nameof(m_visionSeeThroughBrush)}: {m_visionSeeThroughBrush}, " +
                   $"{nameof(m_visionRegionOverride)}: {m_visionRegionOverride}, " +
                   $"{nameof(m_whenToApplyHealing)}: {m_whenToApplyHealing}, " +
                   $"{nameof(m_healPerTurn)}: {m_healPerTurn}, " +
                   $"{nameof(m_healHitSequencePrefab)}: {m_healHitSequencePrefab}, " +
                   $"{nameof(m_spawnForMatchingTeams)}: {m_spawnForMatchingTeams}, " +
                   $"{nameof(m_spawnersForController)}: {m_spawnersForController}, " +
                   $"{nameof(m_autoGenerateBoundaryVisuals)}: {m_autoGenerateBoundaryVisuals}, " +
                   $"{nameof(m_boundaryOscillationSpeed)}: {m_boundaryOscillationSpeed}, " +
                   $"{nameof(m_boundaryOscillationHeight)}: {m_boundaryOscillationHeight}, " +
                   $"{nameof(m_nameplateOverridePosition)}: {m_nameplateOverridePosition}, " +
                   $"{nameof(m_boundaryNeutral)}: {m_boundaryNeutral}, " +
                   $"{nameof(m_boundaryAllied)}: {m_boundaryAllied}, " +
                   $"{nameof(m_boundaryEnemy)}: {m_boundaryEnemy}, " +
                   $"{nameof(m_boundaryDisabled)}: {m_boundaryDisabled}, " +
                   $"{nameof(m_primaryColor_friendly)}: {m_primaryColor_friendly}, " +
                   $"{nameof(m_primaryColor_hostile)}: {m_primaryColor_hostile}, " +
                   $"{nameof(m_primaryColor_neutral)}: {m_primaryColor_neutral}, " +
                   $"{nameof(m_secondaryColor_contested)}: {m_secondaryColor_contested}, " +
                   $"{nameof(m_secondaryColor_friendlyCapturing)}: {m_secondaryColor_friendlyCapturing}, " +
                   $"{nameof(m_secondaryColor_hostileCapturing)}: {m_secondaryColor_hostileCapturing}, " +
                   $"{nameof(m_uiTextColor_Empty)}: {m_uiTextColor_Empty}, " +
                   $"{nameof(m_uiTextColor_Locked)}: {m_uiTextColor_Locked}, " +
                   $"{nameof(m_icon)}: {m_icon}, " +
                   $"{nameof(m_miniMapColorNeutral)}: {m_miniMapColorNeutral}, " +
                   $"{nameof(m_miniMapColorAllied)}: {m_miniMapColorAllied}, " +
                   $"{nameof(m_miniMapColorEnemy)}: {m_miniMapColorEnemy}, " +
                   $"{nameof(m_miniMapColorDisabled)}: {m_miniMapColorDisabled}, " +
                   $"{nameof(m_miniMapImage)}: {m_miniMapImage}, " +
                   $"{nameof(m_currentMinimapColor)}: {m_currentMinimapColor}, " +
                   $"{nameof(m_captureMessages)}: {m_captureMessages}, " +
                   ")";
        }

        public enum State
        {
            Enabled,
            Locked,
            Disabled
        }

        public enum ControlProgressType
        {
            TugOfWar,
            IndependentProgress
        }

        public enum VisionGranting
        {
            Never,
            WhenControlled_ToEveryone,
            WhenControlled_ToControllers,
            WhenControlled_ToOthers,
            AlwaysWhenUnlocked,
            AlwaysIncludingLocked
        }

        public enum CaptureMessageCondition
        {
            OnFriendlyCapture,
            OnEnemyCapture,
            OnEnemyTeamACapture,
            OnFriendlyTeamACapture,
            OnEnemyTeamBCapture,
            OnFriendlyTeamBCapture
        }

        [Serializable]
        public struct CaptureMessage
        {
            public CaptureMessageCondition condition;
            public string message;
            public ColorRGBA color;
        }

        [Serializable]
        public class ControlPointGameplay : ISerializedItem
        {
            public int m_objPoints_uncontested_vacant;
            public int m_objPoints_uncontested_alliesPresent;
            public int m_objPoints_uncontested_enemiesPresent;
            public int m_objPoints_contested_alliesOutnumberEnemies;
            public int m_objPoints_contested_equalEnemiesAndAllies;
            public int m_objPoints_contested_enemiesOutnumberAllies;
            public int m_objPoints_pointsPerAllyOutnumberingEnemy;
            public int m_objPoints_pointsPerEnemyOutnumberingAlly;

            public ControlPointGameplay()
            {
            }

            public ControlPointGameplay(AssetFile assetFile, StreamReader stream)
            {
                DeserializeAsset(assetFile, stream);
            }

            public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
            {
                m_objPoints_uncontested_vacant = stream.ReadInt32();
                m_objPoints_uncontested_alliesPresent = stream.ReadInt32();
                m_objPoints_uncontested_enemiesPresent = stream.ReadInt32();
                m_objPoints_contested_alliesOutnumberEnemies = stream.ReadInt32();
                m_objPoints_contested_equalEnemiesAndAllies = stream.ReadInt32();
                m_objPoints_contested_enemiesOutnumberAllies = stream.ReadInt32();
                m_objPoints_pointsPerAllyOutnumberingEnemy = stream.ReadInt32();
                m_objPoints_pointsPerEnemyOutnumberingAlly = stream.ReadInt32();
            }
        }
    }
}
