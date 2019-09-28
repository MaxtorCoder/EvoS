using System;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(716)]
    public class PlayerInfoUpdateResponse : WebSocketResponseMessage
    {
        public LobbyPlayerInfo PlayerInfo;
        public LobbyCharacterInfo CharacterInfo;
        public LobbyPlayerInfoUpdate OriginalPlayerInfoUpdate;
        public LocalizationPayload LocalizedFailure;
    }
}
