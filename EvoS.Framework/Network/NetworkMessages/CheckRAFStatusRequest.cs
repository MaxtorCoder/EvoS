using System;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(262)]
    public class CheckRAFStatusRequest
    {
        public bool GetReferralCode;
    }
}
