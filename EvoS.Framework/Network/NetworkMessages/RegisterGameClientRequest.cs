using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;
using System.IO;

namespace EvoS.Framework.Network.NetworkMessages
{
	[Serializable]
	public class RegisterGameClientRequest : WebSocketMessage
	{
        public new static int MessageTypeID = 783;

        public LobbySessionInfo SessionInfo;
		public AuthInfo AuthInfo;
		public string SteamUserId;
		public LobbyGameClientSystemInfo SystemInfo;

        public override void HandleMessage(EvosMessageStream message)
        {
            ReadHeader(message);
            this.AuthInfo = AuthInfo.ReadFromStream(message);
        }
    }
}
