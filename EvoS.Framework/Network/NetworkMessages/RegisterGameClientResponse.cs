using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;
using System.IO;

namespace EvoS.Framework.Network.NetworkMessages
{
	[Serializable]
	public class RegisterGameClientResponse : WebSocketResponseMessage
	{
        public new static int MessageTypeID = 778;

        public LobbySessionInfo SessionInfo;
		public AuthInfo AuthInfo;
		public LobbyStatusNotification Status;
		public string DevServerConnectionUrl;
		public LocalizationPayload LocalizedFailure;

        public override void CreateFromStream(EvosMessageStream message)
        {
            throw new NotImplementedException();
        }
    }
}
