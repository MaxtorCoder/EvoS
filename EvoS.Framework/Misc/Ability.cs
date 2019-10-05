using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    public class Ability : MonoBehaviour
    {
        public string m_debugUnlocalizedTooltip = string.Empty;
        public string m_rewardString = string.Empty;
//        private int m_overrideActorDataIndex = ActorData.s_invalidActorIndex;
        public string m_flavorText = string.Empty;
        public string m_expectedSequencePrefixForEditor = string.Empty;
        public int m_initialStockAmount = -1;
        public int m_stockConsumedOnCast = 1;
        public AbilityPriority m_runPriority = AbilityPriority.Combat_Damage;
        public ActorModelData.ActionAnimationType m_actionAnimType = ActorModelData.ActionAnimationType.Ability1;
        public MovementAdjustment m_movementAdjustment = MovementAdjustment.ReducedMovement;
        public float m_movementSpeed = 8f;
        public float m_cameraBoundsMinHeight = 2.5f;
        public List<AbilityTags> m_tags = new List<AbilityTags>();
        public List<StatusType> m_statusWhenRequested = new List<StatusType>();
//        private List<AbilityUtil_Targeter> m_targeters = new List<AbilityUtil_Targeter>();
        private List<GameObject> m_backupTargeterHighlights = new List<GameObject>();
//        private List<AbilityTarget> m_backupTargets = new List<AbilityTarget>();
        public const string c_abilityPreviewVideoPath = "Video/AbilityPreviews/";
        public string m_toolTip;
        public List<StatusType> m_savedStatusTypesForTooltips;
        public string m_shortToolTip;
        private string m_toolTipForUI;
        public GameObject m_sequencePrefab;
//        private ActorData m_actorData;
        private bool m_searchedForActorData;
//        private List<AbilityTooltipNumber> m_abilityTooltipNumbers;
//        private List<AbilityTooltipNumber> m_nameplateTargetingNumbers;
        private bool m_lastUpdateShowingAffectedSquares;
        public bool m_ultimate;
        public string m_previewVideo;
        public int m_cooldown;
        public int m_maxStocks;
        public int m_stockRefreshDuration;
        public bool m_refillAllStockOnRefresh;
        public bool m_abilityManagedStockCount;
        public bool m_freeAction;
        public int m_techPointsCost;
        public TechPointInteraction[] m_techPointInteractions;
        public RotationVisibilityMode m_rotationVisibilityMode;
        public float m_movementDuration;
        public TargetData[] m_targetData;
        public Ability[] m_chainAbilities;
        public List<TargeterTemplateSwapData> m_targeterTemplateSwaps;
        private AbilityMod m_currentAbilityMod;

//        public Sprite sprite { get; set; }

        public enum TargetingParadigm
        {
            Position = 1,
            Direction = 2,
            BoardSquare = 3
        }

        public enum ValidateCheckPath
        {
            Ignore,
            CanBuildPath,
            CanBuildPathAllowThroughInvalid
        }

        public enum RotationVisibilityMode
        {
            Default_NonFreeAction,
            OnAllyClientOnly,
            Always,
            Never
        }

        public enum MovementAdjustment
        {
            FullMovement,
            ReducedMovement,
            NoMovement
        }
    }
}
