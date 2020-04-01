using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(262)]
    public class CheckRAFStatusRequest : WebSocketMessage
    {
        [NonSerialized]
        public new static bool LogData = false;

        public bool GetReferralCode;
    }
}
