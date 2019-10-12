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

        public static void CreateTable()
        {
            Log.Print(LogType.Server, "Creating table Users");
            SQLiteDB.ExecuteNonQuery("CREATE TABLE Users(" +
                "AccountID INTEGER PRIMARY KEY AUTOINCREMENT," +
                "UserName TEXT UNIQUE," +
                "LastSelectedCharacter INT," +
                "SelectedTitleId INT," +
                "SelectedForegroundBannerId INT," +
                "SelectedBackgroundBannerId INT," +
                "SelectedRibbonId INT" +
            ")", null);
        }
        public static Player GetPlayer(string UserName)
        {
            foreach(var reader in SQLiteDB.Query("SELECT AccountId, UserName, LastSelectedCharacter, SelectedTitleId, SelectedForegroundBannerId, SelectedBackgroundBannerId, SelectedRibbonId FROM Users WHERE UserName = ? LIMIT 1", new Dictionary<string, object> {{"UserName",UserName }}))
            {
                var player = new Player();
                player.AccountId = Convert.ToInt64(reader.GetValue(0));
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
            SQLiteDB.ExecuteNonQuery("" +
                "INSERT INTO Users(UserName, LastSelectedCharacter, SelectedTitleId, SelectedForegroundBannerId, SelectedBackgroundbannerId, SelectedRibbonId)" +
                "VALUES(?, ?, -1, -1, -1, -1)", new Dictionary<string, object> { {"UserName",UserName}, {"LastSelectedCharacter",(int)CharacterType.Scoundrel} });
        }

        public static void SaveSelectedCharacter(long AccountId, int CharacterType)
        {
            SQLiteDB.ExecuteNonQuery("UPDATE Users SET LastSelectedCharacter=? WHERE AccountId = ?", new Dictionary<string, object> { {"CharacterType", CharacterType}, {"AccountId", AccountId} });
        }
    }
}
