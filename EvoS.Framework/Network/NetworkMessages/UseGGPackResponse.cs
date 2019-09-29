using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(433)]
    public class UseGGPackResponse : WebSocketResponseMessage
    {
        public string GGPackUserName;
        public int GGPackUserTitle;
        public int GGPackUserTitleLevel;
        public int GGPackUserBannerForeground;
        public int GGPackUserBannerBackground;
        public int GGPackUserRibbon;
        public LocalizationPayload LocalizedFailure;
    }
}
