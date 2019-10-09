namespace EvoS.Framework.Misc
{
    public class CaptureTheFlag
    {
        public enum CTF_VictoryCondition
        {
            TeamMustBeHoldingFlag,
            TeamMustNotBeHoldingFlag,
            OtherTeamMustBeHoldingFlag,
            OtherTeamMustNotBeHoldingFlag,
            TeamMustHaveCapturedFlag,
            TeamMustNotHaveCapturedFlag,
            OtherTeamMustHaveCapturedFlag,
            OtherTeamMustNotHaveCapturedFlag,
        }

        public enum TurninRegionState
        {
            Active,
            Locked,
            Disabled,
        }

        public enum TurninType
        {
            FlagHolderMovingIntoCaptureRegion,
            FlagHolderEndingTurnInCaptureRegion,
            FlagHolderSpendingWholeTurnInCaptureRegion,
            CaptureRegionActivatingUnderFlagHolder,
        }

        public enum RelationshipToClient
        {
            Neutral,
            Friendly,
            Hostile,
        }
    }
}
