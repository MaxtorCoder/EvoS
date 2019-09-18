using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;
using System.IO;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(788, typeof(AssignGameClientRequest))]
    public class AssignGameClientRequest : WebSocketMessage
    {
        public LobbySessionInfo SessionInfo { get; set; }
        public AuthInfo AuthInfo { get; set; }
        public int PreferredLobbyServerIndex { get; set; }

        public new static int MessageTypeID = 788;
    }
}
