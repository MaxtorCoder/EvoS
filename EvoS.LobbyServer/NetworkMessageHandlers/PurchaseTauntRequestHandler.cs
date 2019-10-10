using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.NetworkMessages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    class PurchaseTauntRequestHandler : IEvosNetworkMessageHandler
    {
        public async Task OnMessage(ClientConnection connection, object requestData)
        {
            var request = (PurchaseTauntRequest)requestData;
            var response = new PurchaseTauntResponse()
            {
                Result = PurchaseResult.Success,
                CurrencyType = request.CurrencyType,
                CharacterType = request.CharacterType,
                TauntId = request.TauntId,
                ResponseId = request.RequestId
            };
            await connection.SendMessage(response);
        }
    }
}
