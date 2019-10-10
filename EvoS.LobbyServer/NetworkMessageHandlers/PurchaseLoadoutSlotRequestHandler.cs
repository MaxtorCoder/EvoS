using EvoS.Framework.Network.NetworkMessages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    class PurchaseLoadoutSlotRequestHandler : IEvosNetworkMessageHandler
    {
        public async Task OnMessage(ClientConnection connection, object requestData)
        {
            var response = new PurchaseLoadoutSlotResponse()
            {
                Character = ((PurchaseLoadoutSlotRequest)requestData).Character,
                ResponseId = ((PurchaseLoadoutSlotRequest)requestData).RequestId
            };
            /*I think there are more things to do here*/
            await connection.SendMessage(response);
        }
    }
}
