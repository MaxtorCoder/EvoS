using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(434)]
    public class UseGGPackNotification : WebSocketMessage
    {
        public string GGPackUserName;
        public int NumGGPacksUsed;
        public int GGPackUserTitle;
        public int GGPackUserTitleLevel;
        public int GGPackUserBannerForeground;
        public int GGPackUserBannerBackground;
        public int GGPackUserRibbon;
    }
}
