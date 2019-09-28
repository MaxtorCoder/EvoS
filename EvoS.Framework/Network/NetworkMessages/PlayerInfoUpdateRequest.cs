using System;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(728)]
    public class PlayerInfoUpdateRequest : WebSocketMessage
    {
        public LobbyPlayerInfoUpdate PlayerInfoUpdate;
        [EvosMessage(729)]
        public GameType? GameType;
    }
}
