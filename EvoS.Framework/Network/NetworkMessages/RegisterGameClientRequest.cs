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

        public override void HandleMessage(MemoryStream message)
        {
            BinarySerializer bs = new BinarySerializer();

            ReadHeader(message);
        }
    }
}
