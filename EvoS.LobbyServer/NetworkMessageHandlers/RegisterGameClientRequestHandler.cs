using EvoS.Framework.Network;
using System;
using System.Collections.Generic;
using System.Text;
using EvoS.Framework.Network.NetworkMessages;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    class RegisterGameClientRequestHandler : IEvosNetworkMessageHandler
    {
        public bool DoLogPacket() { return true; }

        public void OnMessage(EvosMessageStream stream)
        {
            RegisterGameClientRequest data = new RegisterGameClientRequest();
            data.CreateFromStream(stream);

            // TODO: Use the data for something useful
            
        }
    }
}
