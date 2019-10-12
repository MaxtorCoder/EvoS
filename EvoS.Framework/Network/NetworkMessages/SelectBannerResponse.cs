using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(401)]
    class SelectBannerResponse : WebSocketResponseMessage
    {
        public int ForegroundBannerID;
        public int BackgroundBannerID;
    }
}
