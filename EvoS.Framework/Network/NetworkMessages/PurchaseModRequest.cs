using System;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(458)]
    public class PurchaseModRequest : WebSocketMessage
    {
        public CharacterType Character;
        public PlayerModData UnlockData;
    }
}
