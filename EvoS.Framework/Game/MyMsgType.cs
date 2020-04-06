using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Game
{
    public enum MyMsgType : short
    {
        ReplayManagerFile = 48,
        DisplayAlert,
        CastAbility,
        LoginRequest, // Sent from the client to the server
        LoginResponse, // Sent from the server to the client
        AssetsLoadedNotification,
        SpawningObjectsNotification,
        ClientPreparedForGameStartNotification,
        ReconnectReplayStatus,
        ObserverMessage,
        StartResolutionPhase,
        ClientResolutionPhaseCompleted,
        ResolveKnockbacksForActor,
        ClientAssetsLoadingProgressUpdate, // Sent from the client to the server
        ServerAssetsLoadingProgressUpdate, // Sent from the server to the clients
        RunResolutionActionsOutsideResolve,
        SingleResolutionAction,
        ClientRequestTimeUpdate,
        Failsafe_HurryResolutionPhase,
        LeaveGameNotification,
        EndGameNotification,
        ServerMovementStarting,
        ClientMovementPhaseCompleted,
        Failsafe_HurryMovementPhase,
        ClashesAtEndOfMovement,
        ClientFakeActionRequest = 32000,
        ServerFakeActionResponse
    }
}
