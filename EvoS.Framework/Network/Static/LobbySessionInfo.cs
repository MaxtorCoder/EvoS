using EvoS.Framework.Constants.Enums;
using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    public class LobbySessionInfo
    {
        public long AccountId { get; set; }
        public string BuildVersion { get; set; }
        public string ConnectionAddress { get; set; }
        public string Handle { get; set; }
        public bool IsBinary { get; set; }
        public string LanguageCode { get; set; }
        public string ProcessCode { get; set; }
        public ProcessType ProcessType { get; set; }
        public string ProtocolVersion { get; set; }
        public Region Region { get; set; }
        public long ReconnectSessionToken { get; set; }
        public long SessionToken { get; set; }
        public string UserName { get; set; }

        public string FakeEntitlements { get; set; }

        public static LobbySessionInfo CreateFromStream(EvosMessageStream stream)
        {
            LobbySessionInfo ret = new LobbySessionInfo();
            int typeId = stream.ReadVarInt();

            ret.AccountId = stream.ReadLong();
            ret.BuildVersion = stream.ReadString();
            ret.ConnectionAddress = stream.ReadString();
            ret.FakeEntitlements = stream.ReadString();
            ret.Handle = stream.ReadString();
            ret.IsBinary = stream.ReadBool();
            ret.LanguageCode = stream.ReadString();
            ret.ProcessCode = stream.ReadString();
            ret.ProcessType = (ProcessType)stream.ReadVarInt();
            ret.ProtocolVersion = stream.ReadString();
            ret.ReconnectSessionToken = stream.ReadLong();
            ret.Region = (Region)stream.ReadVarInt();
            ret.SessionToken = stream.ReadLong();
            ret.UserName = stream.ReadString();

            return ret;
        }
    }

    
}
