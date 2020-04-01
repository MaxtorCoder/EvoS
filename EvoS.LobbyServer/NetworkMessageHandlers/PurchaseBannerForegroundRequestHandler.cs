using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.NetworkMessages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    class PurchaseBannerForegroundRequestHandler : IEvosNetworkMessageHandler
    {
        public async Task OnMessage(ClientConnection connection, object requestData)
        {
            var request = (PurchaseBannerForegroundRequest)requestData;
            var response = new PurchaseBannerForegroundResponse()
            {
                CurrencyType = request.CurrencyType,
                BannerForegroundId = request.BannerForegroundId,
                Result = PurchaseResult.Success,
                ResponseId = request.RequestId
            };
            await connection.SendMessage(response);
        }
    }
}
