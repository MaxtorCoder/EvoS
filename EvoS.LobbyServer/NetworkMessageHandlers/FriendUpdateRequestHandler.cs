using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.NetworkMessages;
using EvoS.LobbyServer.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    class FriendUpdateRequestHandler : IEvosNetworkMessageHandler
    {
        public async Task OnMessage(ClientConnection connection, object requestData)
        {
            var request = (FriendUpdateRequest)requestData;

            switch (request.FriendOperation)
            {
                case FriendOperation.Accept:
                    break;
                case FriendOperation.Add:
                    FriendListUtils.AddFriend(connection, request);
                    break;
                case FriendOperation.Block:
                    break;
                case FriendOperation.Note:
                    break;
                case FriendOperation.Reject:
                    break;
                case FriendOperation.Remove:
                    break;
                case FriendOperation.Unblock:
                    break;
                case FriendOperation.Unknown:
                    break;
            }

            var response = new FriendUpdateResponse()
            {
                FriendAccountId = request.FriendAccountId,
                FriendHandle = request.FriendHandle,
                FriendOperation = request.FriendOperation,
                ResponseId = request.RequestId
            };

            await connection.SendMessage(response);
        }
    }
}
