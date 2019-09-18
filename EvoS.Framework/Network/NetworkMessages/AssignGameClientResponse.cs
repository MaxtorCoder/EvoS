using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;
using System.IO;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(785, typeof(AssignGameClientResponse))]
    public class AssignGameClientResponse : WebSocketResponseMessage
    {
        public new static int MessageTypeID = 785;

        public LobbySessionInfo SessionInfo { get; set; }
        public LobbyGameClientProxyInfo ProxyInfo { get; set; }
        public string LobbyServerAddress { get; set; }
    }
}
