using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.NetworkMessages;
using EvoS.Framework.Network.Static;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    class PurchaseBannerBackgroundRequestHandler : IEvosNetworkMessageHandler
    {
        public async Task OnMessage(ClientConnection connection, object requestData)
        {
            var request = (PurchaseBannerBackgroundRequest)requestData;
            var response = new PurchaseBannerBackgroundResponse()
            {
                CurrencyType = request.CurrencyType,
                BannerBackgroundId = request.BannerBackgroundId,
                Result = PurchaseResult.Success,
                ResponseId = request.RequestId
            };
            await connection.SendMessage(response);

            var update = new InventoryComponentUpdateNotification()
            {
                InventoryComponent = new InventoryComponent()
                {
                    Items = new List<InventoryItem>() { new InventoryItem(request.BannerBackgroundId), new InventoryItem(515,3) }
                },
                RequestId = 0,
            };
            await connection.SendMessage(update);
        }
    }
}
