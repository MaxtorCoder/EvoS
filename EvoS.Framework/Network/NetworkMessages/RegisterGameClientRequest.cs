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

        public AuthInfo AuthInfo;
        public LobbySessionInfo SessionInfo;
		public string SteamUserId;
		public LobbyGameClientSystemInfo SystemInfo;

        public override void CreateFromStream(EvosMessageStream message)
        {
            ReadHeader(message);
            Console.WriteLine("auth");
            this.AuthInfo = AuthInfo.CreateFromStream(message);
            Console.WriteLine("session info");
            this.SessionInfo = LobbySessionInfo.CreateFromStream(message);
            Console.WriteLine("steam");
            this.SteamUserId = message.ReadString();
            Console.WriteLine("sysinfo");
            this.SystemInfo = LobbyGameClientSystemInfo.CreateFromStream(message);
        }
    }
}
