using EvoS.Framework.Constants.Enums;
using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(786, typeof(LobbyGameClientProxyInfo))]
    public class LobbyGameClientProxyInfo
    {
        public long AccountId { get; set; }
        public long SessionToken { get; set; }
        public long AssignmentTime { get; set; }
        public string Handle { get; set; }
        public ClientProxyStatus Status { get; set; }
    }
}
