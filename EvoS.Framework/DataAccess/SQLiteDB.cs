using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;
using EvoS.Framework.Logging;

namespace EvoS.Framework.DataAccess
{
    public class SQLiteDB
    {
        private static string DBName = "EvosDataBase.sqlite3";

        public static void Init()
        {
            if (!File.Exists(DBName))
            {
                CreateDataBase();
            }
        }
        private static SQLiteConnection GetConnection() {
            SQLiteConnection connection = new SQLiteConnection($"Data Source={DBName};Version=3");
            connection.Open();
            return connection;
        }

        private static void CreateDataBase()
        {
            Log.Print(LogType.Warning, "No database found");
            SQLiteConnection.CreateFile(DBName);
            PlayerData.CreateTable();
            ExecuteNonQuery("" +
                "CREATE TABLE AccountComponent(" +
                "   AccountID INT PRIMARY KEY," +
                "   LastCharacter INT," +
                "   SelectedTitleID INT," +
                "   SelectedForegroundBannerID INT," +
                "   SelectedBackgroundBannerID INT," +
                "   SelectedRibbonID INT" +
                ")", null);
        }

        public static void ExecuteNonQuery(string queryString, Dictionary<String, object> params_)
        {
            using (var con = GetConnection())
            {
                using (var command = new SQLiteCommand(queryString, con))
                {
                    if (params_ != null)
                    {
                        foreach (var item in params_)
                        {
                            command.Parameters.Add(new SQLiteParameter(item.Key, item.Value));
                        }
                    }
                    command.ExecuteNonQuery();
                }
            }
        }

        public static System.Collections.Generic.IEnumerable<SQLiteDataReader> Query(string queryString, Dictionary<String, object> params_)
        {
            using (var con = GetConnection()) {
                using (var command = new SQLiteCommand(queryString, con)) {
                    if (params_ != null) {
                        foreach (var item in params_)
                        {
                            command.Parameters.Add(new SQLiteParameter(item.Key, item.Value));
                        }
                    }
                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            yield return reader;
                        }
                    }
                }
            }
        }

        private static void AddParameters(ref SQLiteCommand command, Dictionary<String, object> params_)
        {

        }
    }
}
