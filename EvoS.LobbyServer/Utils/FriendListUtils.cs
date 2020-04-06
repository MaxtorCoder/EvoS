using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.NetworkMessages;
using EvoS.Framework.Network.Static;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public static void AcceptFriend(LobbyServerConnection connection, FriendUpdateRequest request)
        {

        }
        public async static Task AddFriend(LobbyServerConnection connection, FriendUpdateRequest request)
        {
            LobbyServerConnection friend = LobbyServer.GetPlayerByHandle(request.FriendHandle);
            var friendRequest = new FriendUpdateRequest()
            {
                FriendHandle = connection.PlayerInfo.GetHandle(),
                FriendAccountId = connection.PlayerInfo.GetAccountId(),
                FriendOperation = FriendOperation.Add,
                RequestId = 0,
                ResponseId = 0
            };
            await friend.SendMessage(friendRequest);

            // Send a "RequestSent" status to the person that wants to add a new friend
            await connection.SendMessage(new FriendStatusNotification()
            {
                FriendList = new Framework.Network.Static.FriendList()
                {
                    Friends = new Dictionary<long, Framework.Network.Static.FriendInfo>(){
                {
                    0,
                    new Framework.Network.Static.FriendInfo()
                    {
                        FriendHandle = request.FriendHandle,
                        FriendAccountId = request.FriendAccountId,
                        FriendStatus = Framework.Constants.Enums.FriendStatus.RequestSent,
                    }
                }},
                    IsDelta = true// set this to true to the request to tell the client to not overwrite current friend list
                }
            });

            // Send a "RequestReceived" status to the person
            await connection.SendMessage(new FriendStatusNotification()
            {
                FriendList = new Framework.Network.Static.FriendList()
                {
                    Friends = new Dictionary<long, Framework.Network.Static.FriendInfo>(){
                {
                    0,
                    new Framework.Network.Static.FriendInfo()
                    {
                        FriendHandle = connection.PlayerInfo.GetHandle(),
                        FriendAccountId = connection.PlayerInfo.GetAccountId(),
                        FriendStatus = Framework.Constants.Enums.FriendStatus.RequestReceived,
                    }
                }},
                    IsDelta = true // set that this request doesnt have to overwrite friendlist
                }
            });

            // TODO: SEND FRIENDSTATUSNOTIFICATION WITH STATUS REQUESTSENT
        }

        public static void BlockFriend(LobbyServerConnection connection, FriendUpdateRequest request)
        {

        }

        public static void NoteFriend(LobbyServerConnection connection, FriendUpdateRequest request)
        {

        }

        public static void RejectFriend(LobbyServerConnection connection, FriendUpdateRequest request)
        {

        }

        public static void RemoveFriend(LobbyServerConnection connection, FriendUpdateRequest request)
        {

        }

        public static void UnblockFriend(LobbyServerConnection connection, FriendUpdateRequest request)
        {

        }

    }
}
