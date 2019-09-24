using System;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(354)]
    public class FriendInfo
    {
        public FriendInfo()
        {
            BannerID = -1;
            EmblemID = -1;
            TitleID = -1;
            TitleLevel = -1;
            RibbonID = -1;
        }

        public bool IsJoinable(LobbyGameplayOverrides GameplayOverrides)
        {
            return StatusString == "In Game" && GameplayOverrides.AllowSpectatorsOutsideCustom;
        }

        public override string ToString()
        {
            return $"{FriendHandle} ({FriendAccountId}) {FriendStatus.ToString()}";
        }

        public long FriendAccountId;
        public string FriendHandle;
        public FriendStatus FriendStatus;
        public bool IsOnline;
        public string StatusString;
        public string FriendNote;
        public int BannerID;
        public int EmblemID;
        public int TitleID;
        public int TitleLevel;
        public int RibbonID;
    }
}
