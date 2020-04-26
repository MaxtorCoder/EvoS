using EvoS.Framework.Network.NetworkMessages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    class ChatNotificationHandler : IEvosNetworkMessageHandler
    {
        public async Task OnMessage(LobbyServerConnection connection, object requestData)
        {
            ChatNotification notification = (ChatNotification)requestData;
            //notification.SenderAccountId = connection.PlayerInfo.GetAccountId();
            //notification.SenderHandle = connection.PlayerInfo.GetHandle();

            await LobbyServer.sendChatAsync(notification, connection);
        }
    }
}
