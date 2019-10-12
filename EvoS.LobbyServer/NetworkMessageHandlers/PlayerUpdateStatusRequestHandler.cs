using EvoS.Framework.Network.NetworkMessages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    class PlayerUpdateStatusRequestHandler : IEvosNetworkMessageHandler
    {
        public async Task OnMessage(ClientConnection connection, object requestData)
        {
            PlayerUpdateStatusRequest request = (PlayerUpdateStatusRequest)requestData;
            connection.StatusString = request.StatusString;
            var response = new PlayerUpdateStatusResponse()
            {
                AccountId = request.AccountId,
                StatusString = request.StatusString,
                ResponseId = request.RequestId
            };
            await connection.SendMessage(response);
        }
    }
}
