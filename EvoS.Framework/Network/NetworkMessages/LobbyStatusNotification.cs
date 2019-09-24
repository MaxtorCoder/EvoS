using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;
using System.IO;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(768, typeof(LobbyStatusNotification))]
    public class LobbyStatusNotification : WebSocketMessage
    {
        public bool AllowRelogin;
        public ClientAccessLevel ClientAccessLevel;
        public ConnectionQueueInfo ConnectionQueueInfo;
        public TimeSpan? ErrorReportRate;
        public LobbyGameplayOverrides GameplayOverrides;
        public bool HasPurchasedGame;
        public int HighestPurchasedGamePack;
        public LocalizationPayload LocalizedFailure;
        public DateTime PacificNow;
        public ServerLockState ServerLockState;
        public ServerMessageOverrides ServerMessageOverrides;
        public TimeSpan TimeOffset;
        public DateTime UtcNow;
    }
}
