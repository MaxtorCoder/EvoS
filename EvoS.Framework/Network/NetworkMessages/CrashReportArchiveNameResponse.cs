using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(698, typeof(CrashReportArchiveNameResponse))]
    public class CrashReportArchiveNameResponse : WebSocketResponseMessage
    {
        public string ArchiveName;
    }
}
