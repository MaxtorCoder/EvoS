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

        public static void AcceptFriend(ClientConnection connection, FriendUpdateRequest request)
        {

        }
        public async static Task AddFriend(ClientConnection connection, FriendUpdateRequest request)
        {
            ClientConnection friend = LobbyServer.GetPlayerByHandle(request.FriendHandle);
            var friendRequest = new FriendUpdateRequest()
            {
                FriendHandle = connection.UserName,
                FriendAccountId = connection.AccountId,
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
                        FriendHandle = connection.UserName,
                        FriendAccountId = connection.AccountId,
                        FriendStatus = Framework.Constants.Enums.FriendStatus.RequestReceived,
                    }
                }},
                    IsDelta = true // set that this request doesnt have to overwrite friendlist
                }
            });

            // TODO: SEND FRIENDSTATUSNOTIFICATION WITH STATUS REQUESTSENT
        }

        public static void BlockFriend(ClientConnection connection, FriendUpdateRequest request)
        {

        }

        public static void NoteFriend(ClientConnection connection, FriendUpdateRequest request)
        {

        }

        public static void RejectFriend(ClientConnection connection, FriendUpdateRequest request)
        {

        }

        public static void RemoveFriend(ClientConnection connection, FriendUpdateRequest request)
        {

        }

        public static void UnblockFriend(ClientConnection connection, FriendUpdateRequest request)
        {

        }

    }
}
