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
        public async Task OnMessage(LobbyServerConnection connection, object requestData)
        {
            var request = (FriendUpdateRequest)requestData;

            switch (request.FriendOperation)
            {
                case FriendOperation.Accept:
                    FriendListUtils.AcceptFriend(connection, request);
                    break;
                case FriendOperation.Add:
                    await FriendListUtils.AddFriend(connection, request);
                    break;
                case FriendOperation.Block:
                    FriendListUtils.BlockFriend(connection, request);
                    break;
                case FriendOperation.Note:
                    FriendListUtils.NoteFriend(connection, request);
                    break;
                case FriendOperation.Reject:
                    FriendListUtils.RejectFriend(connection, request);
                    break;
                case FriendOperation.Remove:
                    FriendListUtils.RemoveFriend(connection, request);
                    break;
                case FriendOperation.Unblock:
                    FriendListUtils.UnblockFriend(connection, request);
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
