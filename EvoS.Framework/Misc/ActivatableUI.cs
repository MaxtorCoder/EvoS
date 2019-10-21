using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("ActivatableUI")]
    public class ActivatableUI : ISerializedItem
    {
        public UIElement m_uiElement;
        public ActivationAction m_activation;

        public ActivatableUI()
        {
        }

        public ActivatableUI(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_uiElement = (UIElement) stream.ReadInt32();
            m_activation = (ActivationAction) stream.ReadInt32();
        }

        public override string ToString()
        {
            return $"{nameof(ActivatableUI)}>(" +
                   $"{nameof(m_uiElement)}: {m_uiElement}, " +
                   $"{nameof(m_activation)}: {m_activation}, " +
                   ")";
        }

        public enum ActivationAction
        {
            SetActive,
            ClearActive,
            ToggleActive
        }

        public enum UIElement
        {
            TopDisplayPanel,
            DecisionTimer,
            LockInCancelButton,
            AbilityButton,
            AbilityButton1,
            AbilityButton2,
            AbilityButton3,
            AbilityButton4,
            QueuedCard,
            QueuedCard1,
            QueuedCard2,
            Taunt,
            TopDisplayPanelBackground,
            TopDisplayPanelCenterPiece,
            TopDisplayPanelPlayerStatus,
            TopDisplayPanelPlayerStatus1,
            TopDisplayPanelPlayerStatus2,
            TopDisplayPanelPlayerStatus3,
            TopDisplayPanelPlayerStatus4,
            TopDisplayPanelPlayerStatus5,
            ObjectivePanel,
            AbilityButtonGlow,
            AbilityButtonGlow1,
            AbilityButtonGlow2,
            AbilityButtonGlow3,
            AbilityButtonGlow4,
            AbilityButtonTutorialTip,
            AbilityButtonTutorialTip1,
            AbilityButtonTutorialTip2,
            AbilityButtonTutorialTip3,
            AbilityButtonTutorialTip4,
            LockInButtonTutorialTip,
            CameraControlsTutorialPanel,
            EnergyGlow,
            EnergyArrows,
            CombatPhaseTutorialPanel,
            DashPhaseTutorialPanel,
            PrepPhaseTutorialPanel,
            NotificationPanel,
            BuffList,
            FullScreenCombatPhaseTutorialPanel,
            FullScreenDashPhaseTutorialPanel,
            FullScreenPrepPhaseTutorialPanel,
            LockInButtonTutorialTipImage,
            LockInButtonTutorialTipText,
            LockinPhaseDisplay,
            LockinPhaseTextDisplay,
            LockinPhaseColorDisplay,
            CardButtonTutorialTip,
            CardButtonTutorialTip1,
            CardButtonTutorialTip2,
            StatusEffectTutorialPanel,
            TeammateTargetingTutorialPanel,
            EnergyAndUltimatesTutorialPanel,
            FadeOutPanel,
            ResetMatchTimer,
            ResetMatchTurn
        }
    }
}
