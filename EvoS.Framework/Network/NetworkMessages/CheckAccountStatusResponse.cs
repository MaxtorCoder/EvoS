using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(268)]
    public class CheckAccountStatusResponse : WebSocketResponseMessage
    {
        public QuestOfferNotification QuestOffers;
    }
}
