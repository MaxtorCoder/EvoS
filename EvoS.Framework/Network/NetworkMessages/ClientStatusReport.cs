using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(696, typeof(ClientStatusReport))]
    public class ClientStatusReport : WebSocketMessage
    {
        [NonSerialized]
        public new static bool LogData = false;

        public string DeviceIdentifier;
        public string FileDateTime;
        public ClientStatusReport.ClientStatusReportType Status;
        public string StatusDetails;
        public string UserMessage;

        [Serializable]
        [EvosMessage(697, typeof(ClientStatusReport.ClientStatusReportType))]
        public enum ClientStatusReportType
        {
			u001D,
			u000E,
			u0012,
			u0015,
			u0016
		}
    }
}
