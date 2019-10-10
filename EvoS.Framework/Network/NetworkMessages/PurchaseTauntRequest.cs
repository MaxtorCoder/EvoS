using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(297)]
    public class PurchaseTauntRequest : WebSocketMessage
    {
        public CurrencyType CurrencyType;
        public CharacterType CharacterType;
        public int TauntId;
    }
}
