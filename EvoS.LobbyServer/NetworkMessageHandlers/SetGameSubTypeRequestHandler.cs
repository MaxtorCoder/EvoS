using EvoS.Framework.Network.NetworkMessages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    class SetGameSubTypeRequestHandler : IEvosNetworkMessageHandler
    {
        public async Task OnMessage(ClientConnection connection, object requestData)
        {
            SetGameSubTypeRequest request = (SetGameSubTypeRequest)requestData;
            connection.SelectedGameType = request.gameType;
            await connection.SendMessage(new SetGameSubTypeResponse() { ResponseId = request.RequestId });
        }
    }
}
