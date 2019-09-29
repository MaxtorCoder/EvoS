using System;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(503)]
    public class PlayerCharacterDataUpdateNotification : WebSocketResponseMessage
    {
        public PersistedCharacterData CharacterData;
    }
}
