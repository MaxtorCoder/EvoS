using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;
using System.IO;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(783, typeof(RegisterGameClientRequest))]
    public class RegisterGameClientRequest : WebSocketMessage
    {
        public new static int MessageTypeID = 783;
        [NonSerialized]
        public new static bool LogData = false;

        public AuthInfo AuthInfo;
        public LobbySessionInfo SessionInfo;
        public string SteamUserId;
        public LobbyGameClientSystemInfo SystemInfo;
    }
}
