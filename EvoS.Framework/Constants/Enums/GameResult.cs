using EvoS.Framework.Network;

namespace EvoS.Framework.Constants.Enums
{
    [EvosMessage(199)]
    public enum GameResult
    {
        NoResult,
        TieGame,
        TeamAWon,
        TeamBWon,
        Requeued,
        OwnerLeft,
        ServerCrashed,
        ClientKicked,
        ClientLeft,
        ClientShutDown,
        EmptyPlayers,
        ClientConnectionFailedToLobbyServer,
        ClientConnectionFailedToGameServer,
        ClientNetworkErrorToLobbyServer,
        ClientNetworkErrorToGameServer,
        ClientHeartbeatTimeoutToLobbyServer,
        ClientHeartbeatTimeoutToGameServer,
        ClientLoginFailedToLobbyServer,
        ClientLoginFailedToGameServer,
        ClientLoadingTimeout,
        ClientDisconnectedAtLaunch,
        LobbyServerShutdown,
        LobbyServerKickAccount,
        LobbyServerNetworkErrorToClient,
        LobbyServerHeartbeatTimeoutToClient,
        GameServerNetworkErrorToClient,
        GameServerHeartbeatTimeoutToClient,
        GameServerNetworkErrorToMonitorServer,
        GameServerHeartbeatTimeoutToMonitorServer,
        ClientIdleTimeout,
        SkippedTurnsIdleTimeout
    }
}
