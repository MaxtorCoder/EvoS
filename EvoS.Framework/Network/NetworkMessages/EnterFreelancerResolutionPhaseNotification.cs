using System;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(183)]
    public class EnterFreelancerResolutionPhaseNotification : WebSocketMessage
    {
        public FreelancerResolutionPhaseSubType SubPhase;
        [EvosMessage(185)]
        public RankedResolutionPhaseData? RankedData;
    }
}
