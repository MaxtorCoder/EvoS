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
            RegisterGameClientRequest request = new RegisterGameClientRequest();
            request.CreateFromStream(stream);

            RegisterGameClientResponse response = new RegisterGameClientResponse();

            response.SessionInfo = request.SessionInfo;
            response.SessionInfo.ConnectionAddress = "127.0.0.1";
            response.AuthInfo = request.AuthInfo;
            response.Status = new LobbyStatusNotification();
            response.DevServerConnectionUrl = "127.0.0.1"; // What is this?
            response.LocalizedFailure = null;

            response.WriteData(stream);

        }
    }
}
