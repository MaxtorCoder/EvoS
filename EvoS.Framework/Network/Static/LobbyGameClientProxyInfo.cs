using EvoS.Framework.Network.Shared;
using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    public class LobbyGameClientProxyInfo
    {
        public long AccountId { get; set; }
        public long SessionToken { get; set; }
        public long AssignmentTime { get; set; }
        public string Handle { get; set; }
        public ClientProxyStatus Status { get; set; }
    }
}
