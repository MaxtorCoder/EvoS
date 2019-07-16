using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    public class AssignGameClientRequest : WebSocketMessage
    {
        public LobbySessionInfo SessionInfo { get; set; }
        public AuthInfo AuthInfo { get; set; }
        public int PreferredLobbyServerIndex { get; set; }
    }
}
