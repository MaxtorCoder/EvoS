using System;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(553)]
    public class PlayerAccountDataUpdateNotification : WebSocketResponseMessage
    {
        public PersistedAccountData AccountData;
    }
}
