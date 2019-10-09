using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    class OptionsNotificationHandler : IEvosNetworkMessageHandler
    {
        public static bool LogData = false;
        public bool DoLogPacket()
        {
            throw new NotImplementedException();
        }

        public Task OnMessage(ClientConnection connection, object requestData)
        {
            return Task.CompletedTask;
        }
    }
}
