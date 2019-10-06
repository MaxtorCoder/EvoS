using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;


namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(692, typeof(ClientErrorSummary))]
    public class ClientErrorSummary : WebSocketMessage
    {
        public Dictionary<uint, uint> ReportCount;
    }
}
