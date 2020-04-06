using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    class PricesRequestHandler : IEvosNetworkMessageHandler
    {
        public Task OnMessage(LobbyServerConnection connection, object requestData)
        {
            return Task.CompletedTask;
        }
    }
}
