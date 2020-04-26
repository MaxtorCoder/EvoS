using System;
using System.Collections.Generic;
using System.Text;

namespace CentralServer.LobbyServer.Database
{
    public class Account
    {
        private static long FakeAccountId = 0;

        public long AccountId;
        public string UserName;
        public CharacterType LastCharacter = Config.ConfigManager.DefaultCharacterType;
        public int TitleID = -1;
        public int BannerID = -1;
        public int EmblemID = -1;
        public int RibbonID = -1;
        public GameType LastSelectedGameType = Config.ConfigManager.DefaultGameType;

        

        private Account() { }

        public static Account GetByAccountId(long accountID)
        {
            // TODO
            Account user = new Account();
            user.AccountId = accountID;
            user.UserName = "DefaultName";

            return user;
        }

        public static Account GetByUserName(string username)
        {
            // TODO
            Account user = new Account()
            {
                AccountId = FakeAccountId++,
                UserName = username
            };

            return user;
        }
    }
}
