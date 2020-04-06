using EvoS.Framework.Network.NetworkMessages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    class SetGameSubTypeRequestHandler : IEvosNetworkMessageHandler
    {
        public async Task OnMessage(LobbyServerConnection connection, object requestData)
        {
            SetGameSubTypeRequest request = (SetGameSubTypeRequest)requestData;
            connection.PlayerInfo.SetGameType(request.gameType);
            connection.PlayerInfo.SetSubTypeMask(request.SubTypeMask);
            
            await connection.SendMessage(new SetGameSubTypeResponse() { ResponseId = request.RequestId });
        }
    }
}
