using System;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(486)]
    public class PreviousGameInfoResponse : WebSocketResponseMessage
    {
        public LobbyGameInfo PreviousGameInfo;
    }
}
