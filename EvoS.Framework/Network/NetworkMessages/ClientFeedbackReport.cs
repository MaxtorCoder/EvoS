using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;


namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(687, typeof(ClientFeedbackReport))]
    public class ClientFeedbackReport : WebSocketMessage
    {
        public string Message;
        public ClientFeedbackReport.FeedbackReason Reason;
        public long ReportedPlayerAccountId;
        public string ReportedPlayerHandle;

        [Serializable]
        [EvosMessage(688, typeof(ClientFeedbackReport.FeedbackReason))]
        public enum FeedbackReason
        {
			Suggestion,
			Bug,
			u0012,
			u0015,
			u0016,
			u0013,
			u0018,
			u0009,
			u0019,
			u0011,
			u001A,
			u0004
		}
    }
}
