using EvoS.Framework.Constants.Enums;
using System;
using System.IO;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    public class AuthInfo
    {   
        public string AccountCurrency { get; set; }
        public long AccountId { get; set; }
        public string AccountStatus { get; set; }
        public string Handle { get; set; }
        public string Password { get; set; }
        public string TicketData { get; set; }
        public AuthType Type { get; set; }
        public string UserName { get; set; }

        public long SteamId { get; set; }


        public static AuthInfo ReadFromStream(EvosMessageStream stream)
        {
            AuthInfo ret = new AuthInfo();
            BinarySerializer bs = new BinarySerializer();
            int typeId = stream.ReadVarInt();

            ret.AccountCurrency = stream.ReadString();
            ret.AccountId = stream.ReadVarInt();
            ret.AccountStatus = stream.ReadString();
            ret.Handle = stream.ReadString();
            ret.Password = stream.ReadString();
            ret.SteamId = stream.ReadVarInt();
            ret.TicketData = stream.ReadString();
            ret.Type = (AuthType)stream.ReadVarInt();
            ret.UserName = stream.ReadString();

            return ret;
        }
    }
}
