using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(289)]
    public class PurchaseBannerForegroundRequest : WebSocketMessage
    {
        public CurrencyType CurrencyType;
        public int BannerForegroundId;
    }
}
