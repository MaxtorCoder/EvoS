using System;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(551)]
    public class ForcedCharacterChangeFromServerNotification : WebSocketMessage
    {
        public LobbyCharacterInfo ChararacterInfo;
    }
}
