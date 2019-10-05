using EvoS.Framework.Network.NetworkMessages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    class ChatNotificationHandler : IEvosNetworkMessageHandler
    {
        public bool DoLogPacket()
        {
            return false;
        }

        public async Task OnMessage(ClientConnection connection, object requestData)
        {
            await LobbyServer.Program.sendChatAsync((ChatNotification)requestData, connection);
        }
    }
}
