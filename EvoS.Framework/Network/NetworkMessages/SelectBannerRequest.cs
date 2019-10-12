using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(402)]
    public class SelectBannerRequest : WebSocketMessage
    {
        public int BannerID;
    }
}
