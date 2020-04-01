using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(290)]
    public class PurchaseBannerBackgroundResponse : WebSocketResponseMessage
    {
        public PurchaseResult Result;
        public CurrencyType CurrencyType;
        public int BannerBackgroundId;
    }
}
