using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.NetworkMessages;
using EvoS.Framework.Network.Static;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    class JoinMatchmakingQueueRequestHandler : IEvosNetworkMessageHandler
    {
        public async Task OnMessage(ClientConnection connection, object requestData)
        {
            var request = (JoinMatchmakingQueueRequest)requestData;
            await connection.SendMessage(new JoinMatchmakingQueueResponse() { ResponseId = request.RequestId });

            LobbyQueueManager.AddPlayerToQueue(connection);
        }
    }
}
