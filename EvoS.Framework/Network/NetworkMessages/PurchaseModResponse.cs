using System;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(457)]
    public class PurchaseModResponse : WebSocketResponseMessage
    {
        public CharacterType Character;
        public PlayerModData UnlockData;
    }
}
