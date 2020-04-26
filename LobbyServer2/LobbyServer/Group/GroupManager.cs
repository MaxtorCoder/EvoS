using CentralServer.LobbyServer.Account;
using CentralServer.LobbyServer.Session;
using EvoS.Framework.Network.Static;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentralServer.LobbyServer.Group
{
    class GroupManager
    {
        public static LobbyPlayerGroupInfo GetGroupInfo(long accountId)
        {
            // TODO
            LobbyServerProtocol client = SessionManager.GetClientConnection(accountId);

            LobbyPlayerGroupInfo groupInfo = new LobbyPlayerGroupInfo()
            {
                SelectedQueueType = client.SelectedGameType,
                MemberDisplayName = client.UserName,
                //InAGroup = false,
                //IsLeader = true,
                Members = new List<UpdateGroupMemberData>(),
            };

            return groupInfo;
        }
    }
}
