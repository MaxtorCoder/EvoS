using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    public class AssignGameClientResponse : WebSocketResponseMessage
    {
        public LobbySessionInfo SessionInfo { get; set; }
        public LobbyGameClientProxyInfo ProxyInfo { get; set; }
        public string LobbyServerAddress { get; set; }
    }
}
