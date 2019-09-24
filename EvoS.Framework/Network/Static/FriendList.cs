using System;
using System.Collections.Generic;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(358)]
    public class FriendList
    {
        [EvosMessage(359)]
        public Dictionary<long, FriendInfo> Friends = new Dictionary<long, FriendInfo>();
        public bool IsDelta;
        public bool IsError;
    }
}
