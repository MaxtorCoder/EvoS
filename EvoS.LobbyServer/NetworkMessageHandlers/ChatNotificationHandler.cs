using EvoS.Framework.Network.NetworkMessages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    class ChatNotificationHandler : IEvosNetworkMessageHandler
    {
        public async Task OnMessage(ClientConnection connection, object requestData)
        {
            ChatNotification notification = (ChatNotification)requestData;
            notification.SenderAccountId = connection.AccountId;
            notification.SenderHandle = connection.UserName;

            await LobbyServer.sendChatAsync(notification, connection);
        }
    }
}
