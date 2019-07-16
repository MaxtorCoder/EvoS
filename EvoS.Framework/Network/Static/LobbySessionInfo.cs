using EvoS.Framework.Network.Shared;
using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    public class LobbySessionInfo
    {
        public long AccountId { get; set; }
        public string UserName { get; set; }
        public string BuildVersion { get; set; }
        public string ProtocolVersion { get; set; }
        public long SessionToken { get; set; }
        public long ReconnectSessionToken { get; set; }
        public string ProcessCode { get; set; }
        public ProcessType ProcessType { get; set; }
        public string ConnectionAddress { get; set; }
        public string Handle { get; set; }
        public bool IsBinary { get; set; }
        public string FakeEntitlements { get; set; }
        public Region Region { get; set; }
        public string LanguageCode { get; set; }
    }
}
