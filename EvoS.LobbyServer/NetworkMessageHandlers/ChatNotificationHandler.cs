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
            ChatNotification notification = (ChatNotification)requestData;
            notification.SenderAccountId = connection.AuthInfo.AccountId;
            notification.SenderHandle = connection.AuthInfo.Handle;

            await LobbyServer.Program.sendChatAsync(notification, connection);
        }
    }
}
