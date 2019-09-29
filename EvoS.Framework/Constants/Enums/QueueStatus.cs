using EvoS.Framework.Network;

namespace EvoS.Framework.Constants.Enums
{
    [EvosMessage(745)]
    public enum QueueStatus
    {
        Idle,
        Success,
        WaitingForHumans,
        QueueDoesntHaveEnoughHumans,
        BadOldestEntry,
        WontMixNoobAndExpert,
        EloRangeTooExtreme,
        WaitingForRoleAssassin,
        WaitingForRoleTank,
        WaitingForRoleSupport,
        WaitingOnImbalancedRoleAssassin,
        WaitingOnImbalancedRoleTank,
        WaitingOnImbalancedRoleSupport,
        WaitingOnImbalancedRoleGeneric,
        BlockedByExpertCollisions,
        BlockedByNoobCollisions,
        WaitingOnImbalancedGroups,
        WaitingToBreakGroups,
        TeamBalanceError,
        AllServersBusy,
        TeamEloTooExtreme,
        NeedDifferentOpponents,
        NeedDifferentCoopDifficulties,
        TierTooHighForPlacementPlayers,
        TierBreadthTooExtreme,
        TooManyWillFills,
        NotEnoughWillFills,
        SubQueueConflict,
        MultipleRegions,
        WaitingOnPerfectGroupComposition,
        Unknown
    }
}
