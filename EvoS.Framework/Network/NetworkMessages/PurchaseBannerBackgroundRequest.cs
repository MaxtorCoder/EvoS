using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(291)]
    public class PurchaseBannerBackgroundRequest : WebSocketMessage
    {
        public CurrencyType CurrencyType;
        public int BannerBackgroundId;
    }
}
