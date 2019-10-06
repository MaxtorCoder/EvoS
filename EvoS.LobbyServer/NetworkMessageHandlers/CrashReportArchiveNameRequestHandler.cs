using EvoS.Framework.Network.NetworkMessages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    class CrashReportArchiveNameRequestHandler : IEvosNetworkMessageHandler
    {
        public bool DoLogPacket()
        {
            throw new NotImplementedException();
        }

        public async Task OnMessage(ClientConnection connection, object requestData)
        {
            CrashReportArchiveNameResponse response = new CrashReportArchiveNameResponse()
            {
                ArchiveName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt",
                ResponseId = ((CrashReportArchiveNameRequest)requestData).RequestId
            };
            await connection.SendMessage(response);
        }
    }
}
