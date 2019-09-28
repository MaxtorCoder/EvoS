using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(261)]
    public class CheckRAFStatusResponse : WebSocketResponseMessage
    {
        public string ReferralCode;
    }
}
