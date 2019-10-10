using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(296)]
    public class PurchaseTauntResponse : WebSocketResponseMessage
    {
        public PurchaseResult Result;
        public CurrencyType CurrencyType;
        public CharacterType CharacterType;
        public int TauntId;
    }
}
