using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using EvoS.Framework.Network;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    interface IEvosNetworkMessageHandler
    {
        Task OnMessage(LobbyServerConnection connection, object requestData);
    }
}
