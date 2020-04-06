using EvoS.Framework.Game;
using EvoS.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.DataAccess
{
    public class PlayerData
    {
        public class Player
        {
            public long AccountId;
            public string UserName;
            public CharacterType LastSelectedCharacter;
            public int SelectedTitleID;
            public int SelectedForegroundBannerID;
            public int SelectedBackgroundBannerID;
            public int SelectedRibbonID;
        }

        public static Player GetPlayer(string UserName)
        {
            if (!EvoSGameConfig.UseDatabase) {
                return new Player()
                {
                    AccountId = DateTime.Now.Ticks,
                    UserName = UserName,
                    LastSelectedCharacter = CharacterType.BazookaGirl,
                    SelectedTitleID = -1,
                    SelectedForegroundBannerID = -1,
                    SelectedBackgroundBannerID = -1,
                    SelectedRibbonID = -1
                };
            }
            foreach(var reader in MySQLDB.GetInstance().Query("SELECT AccountId, UserName, LastSelectedCharacter, SelectedTitleId, BannerForegroundId, BannerBackgroundId, RibbonId FROM Users WHERE UserName = ? LIMIT 1", new object[] {UserName}))
            {
                var player = new Player();
                player.AccountId = reader.GetInt32(0);
                player.UserName = reader.GetString(1);
                player.LastSelectedCharacter = (CharacterType)reader.GetInt32(2);
                player.SelectedTitleID = reader.GetInt32(3);
                player.SelectedForegroundBannerID = reader.GetInt32(4);
                player.SelectedBackgroundBannerID = reader.GetInt32(5);
                player.SelectedRibbonID = reader.GetInt32(6);
                return player;
            }
            return null;
        }

        public static void CreatePlayer(string UserName)
        {
            MySQLDB.GetInstance().ExecuteNonQuery("INSERT INTO Users(UserName, LastSelectedCharacter, SelectedTitleId, BannerForegroundId, BannerBackgroundId, RibbonId)" +"VALUES(?, ?, -1, -1, -1, -1)",  new object[]{UserName, (int)CharacterType.Scoundrel});
        }

        public static void SaveSelectedCharacter(long AccountId, int CharacterType)
        {
            MySQLDB.GetInstance().ExecuteNonQuery("UPDATE Users SET LastSelectedCharacter=? WHERE AccountId = ?", new object[] {CharacterType, AccountId});
        }
    }
}
