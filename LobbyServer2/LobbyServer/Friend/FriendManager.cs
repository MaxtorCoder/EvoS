using EvoS.Framework.Network.NetworkMessages;
using EvoS.Framework.Network.Static;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentralServer.LobbyServer.Friend
{
    class FriendManager
    {
        public static FriendStatusNotification GetFriendStatusNotification(long accountId)
        {
            FriendStatusNotification notification = new FriendStatusNotification()
            {
                FriendList = GetFriendList(accountId)
            };

            return notification;
        }

        public static FriendList GetFriendList(long accountId)
        {
            FriendList friendList = new FriendList()
            {
                Friends = new Dictionary<long, FriendInfo>(), // TODO
                IsDelta = false
            };

            return friendList;
        }

        public static PlayerUpdateStatusResponse OnPlayerUpdateStatusRequest(LobbyServerProtocol client, PlayerUpdateStatusRequest request)
        {
            // TODO: notify this client's friends the status change

            PlayerUpdateStatusResponse response = new PlayerUpdateStatusResponse()
            {
                AccountId = client.AccountId,
                StatusString = request.StatusString,
                ResponseId = request.RequestId
            };

            return response;
        }
    }
}
