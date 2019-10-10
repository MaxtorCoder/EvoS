using EvoS.Framework.Network.NetworkMessages;
using EvoS.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    class ClientFeedbackReportHandler : IEvosNetworkMessageHandler
    {
        public Task OnMessage(ClientConnection connection, object requestData)
        {
            ClientFeedbackReport feedback = (ClientFeedbackReport)requestData;
            if (feedback.Reason == ClientFeedbackReport.FeedbackReason.Suggestion)
                Log.Print(LogType.Lobby, $"{connection.RegistrationInfo.SessionInfo.UserName} sent a suggestion: {feedback.Message}");
            else if (feedback.Reason == ClientFeedbackReport.FeedbackReason.Bug)
                Log.Print(LogType.Lobby, $"{connection.RegistrationInfo.SessionInfo.UserName} sent a bug report: {feedback.Message}");
            return Task.CompletedTask;
        }
    }
}
