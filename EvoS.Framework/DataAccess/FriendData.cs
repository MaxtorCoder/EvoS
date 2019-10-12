using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Static;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.DataAccess
{
    public class FriendData
    {
        public static FriendList GetFriendList(long AccountId)
        {
            var friendList = new FriendList();
            friendList.IsDelta = false;
            friendList.Friends = new Dictionary<long, FriendInfo>();

            foreach (var item in MySQLDB.GetInstance().Query($"SELECT Users.AccountId, Users.UserName, Users.BannerForegroundId as EmblemId, Users.BannerBackgroundId as BannerID, Users.SelectedTitleId as TitleId, Users.RibbonId, FriendList.FriendStatus FROM FriendList INNER JOIN Users ON(Users.AccountId = AccountID_1 or Users.AccountId = AccountID_2) AND Users.AccountID <> {AccountId} WHERE AccountID_1 = {AccountId} or (AccountID_2 = {AccountId} and FriendStatus=1) ORDER BY FriendStatus", new object[] {})) {
                var info = new FriendInfo()
                {
                    FriendAccountId = item.GetInt32(0),
                    FriendHandle = item.GetString(1),
                    IsOnline = true,
                    FriendStatus = (FriendStatus)item.GetByte(6),
                    BannerID = item.GetInt16(3),
                    EmblemID = item.GetInt16(2),
                    TitleID = item.GetInt16(4),
                    RibbonID = item.GetInt16(5),
                    StatusString = "Online"
                };
                friendList.Friends[info.FriendAccountId] = info;
            }

            return friendList;
        }
    }
}
