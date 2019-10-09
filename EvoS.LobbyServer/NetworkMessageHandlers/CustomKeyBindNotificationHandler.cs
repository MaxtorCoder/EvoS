using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    class CustomKeyBindNotificationHandler : IEvosNetworkMessageHandler
    {
        public bool DoLogPacket()
        {
            return false;
        }

        public Task OnMessage(ClientConnection connection, object requestData)
        {
            return Task.CompletedTask;
        }
    }
}
