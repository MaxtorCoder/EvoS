using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;
using System.IO;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    public class AssignGameClientResponse : WebSocketResponseMessage
    {
        public new static int MessageTypeID = 785;

        public LobbySessionInfo SessionInfo { get; set; }
        public LobbyGameClientProxyInfo ProxyInfo { get; set; }
        public string LobbyServerAddress { get; set; }

        public override void HandleMessage(EvosMessageStream message)
        {
            throw new NotImplementedException();
        }
    }
}
