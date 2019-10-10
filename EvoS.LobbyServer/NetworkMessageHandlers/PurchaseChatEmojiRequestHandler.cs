using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.NetworkMessages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    class PurchaseChatEmojiRequestHandler : IEvosNetworkMessageHandler
    {
        public async Task OnMessage(ClientConnection connection, object requestData)
        {
            var request = (PurchaseChatEmojiRequest)requestData;
            var response = new PurchaseChatEmojiResponse()
            {
                Result = PurchaseResult.Success,
                CurrencyType = request.CurrencyType,
                EmojiID = request.EmojiID,
                ResponseId = request.RequestId
            };
            await connection.SendMessage(response);
        }
    }
}
