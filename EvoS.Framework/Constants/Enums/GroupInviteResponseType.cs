using EvoS.Framework.Network;

namespace EvoS.Framework.Constants.Enums
{
    [EvosMessage(373)]
    public enum GroupInviteResponseType
    {
        UNKNOWN,
        PlayerAccepted,
        PlayerStillAwaitingPreviousQuery,
        PlayerInCustomMatch,
        PlayerRejected,
        OfferExpired,
        RequestorSpamming
    }
}
