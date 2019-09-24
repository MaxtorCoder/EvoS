using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EvoS.Framework.Network;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    interface IEvosNetworkMessageHandler
    {
        void OnMessage(object requestData, MemoryStream stream);
        bool DoLogPacket();
    }
}
