using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;
using System.IO;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(778, typeof(RegisterGameClientResponse))]
    public class RegisterGameClientResponse : WebSocketResponseMessage
    {
        public AuthInfo AuthInfo;
        public string DevServerConnectionUrl;
        public LocalizationPayload LocalizedFailure;
        public LobbySessionInfo SessionInfo;
        public LobbyStatusNotification Status;
    }
}
