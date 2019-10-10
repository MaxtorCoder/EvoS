using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.NetworkMessages;
using EvoS.Framework.Network.Static;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.LobbyServer.Utils
{
    class FriendListUtils
    {
        public static FriendList GetFriendList(long AccountId)
        {
            return new FriendList()
            {
                Friends = new Dictionary<long, FriendInfo>()
                {
                    { 1, new FriendInfo() { FriendHandle = "TestFriend", IsOnline = true, FriendStatus = FriendStatus.Friend, StatusString = "Online" } }
                }
            };
        }

        public static void AcceptFriend(ClientConnection connection, FriendUpdateRequest request)
        {

        }
        public static void AddFriend(ClientConnection connection, FriendUpdateRequest request)
        {

        }

    }
}
