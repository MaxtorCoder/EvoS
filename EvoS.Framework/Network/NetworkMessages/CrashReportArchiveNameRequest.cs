using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(699, typeof(CrashReportArchiveNameRequest))]
    public class CrashReportArchiveNameRequest : WebSocketMessage
    {
        public int NumArchiveBytes;
        public bool DevBuild;
    }
}
