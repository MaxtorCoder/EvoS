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
            JoinMatchmakingQueueRequest request = (JoinMatchmakingQueueRequest)requestData;

            QueueManager.AddPlayerToQueue(request.GameType, connection);

            JoinMatchmakingQueueResponse response = new JoinMatchmakingQueueResponse();
            response.ResponseId = request.RequestId;
            //response.RequestId = request.ResponseId;
            await connection.SendMessage(response);

            MatchmakingQueueAssignmentNotification assignmentNotification = new MatchmakingQueueAssignmentNotification()
            {
                Reason = "",
                MatchmakingQueueInfo = new LobbyMatchmakingQueueInfo()
                {
                    ShowQueueSize = true,
                    QueuedPlayers = 7,
                    PlayersPerMinute = 4,
                    GameConfig = new LobbyGameConfig(),
                    QueueStatus = QueueStatus.Success,
                    AverageWaitTime = TimeSpan.FromSeconds(100),
                    
                }
            };
            await connection.SendMessage(assignmentNotification);
        }

        
    }
}
