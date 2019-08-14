using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;
using System.IO;

namespace EvoS.Framework.Network.NetworkMessages
{
	// Token: 0x0200080E RID: 2062
	[Serializable]
	public class LobbyStatusNotification : WebSocketMessage
	{
		public LocalizationPayload LocalizedFailure;
		public bool AllowRelogin;
		public ServerLockState ServerLockState;
		public ConnectionQueueInfo ConnectionQueueInfo;
		public ClientAccessLevel ClientAccessLevel;
		public bool HasPurchasedGame;
		public int HighestPurchasedGamePack;
		public ServerMessageOverrides ServerMessageOverrides;
		public LobbyGameplayOverrides GameplayOverrides;
		public DateTime UtcNow;
		public DateTime PacificNow;
		public TimeSpan TimeOffset;
		public TimeSpan? ErrorReportRate;

        public override void CreateFromStream(EvosMessageStream message)
        {
            throw new NotImplementedException();
        }
    }
}
