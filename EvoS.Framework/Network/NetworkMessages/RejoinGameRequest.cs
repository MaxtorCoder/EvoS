using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(475)]
    public class RejoinGameRequest : WebSocketMessage
    {
        public LobbyGameInfo PreviousGameInfo;
        public bool Accept;
    }
}
