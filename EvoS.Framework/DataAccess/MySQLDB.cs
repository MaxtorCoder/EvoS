using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace EvoS.Framework.DataAccess
{
    public class MySQLDB
    {
        private MySqlConnection connection = null;
        private static MySQLDB Instance;
        private String SERVER_ADDRESS = "192.168.1.6";
        private int SERVER_PORT = 3306;
        private String USERNAME = "root";
        private String PASSWORD = "root";

        private MySQLDB()
        {
            connection = new MySqlConnection($"Server={SERVER_ADDRESS};Port={SERVER_PORT};Database=Evos;Uid={USERNAME};Pwd={PASSWORD}; ");
            connection.Open();
        }

        public static MySQLDB GetInstance()
        {
            if (Instance == null)
            {
                Instance = new MySQLDB();
            }
            return Instance;
        }

        public async void ExecuteNonQuery(String queryString, object[] Params)
        {
            MySqlCommand command = new MySqlCommand(queryString,connection);
            if (Params != null)
                for (int i = 0; i < Params.Length; i++)
                    command.Parameters.Add(new MySqlParameter(i.ToString(), Params[i]));
            
            await command.ExecuteNonQueryAsync();
        }

        public IEnumerable<MySqlDataReader> Query(String queryString, object[] Params)
        {
            MySqlCommand command = new MySqlCommand(queryString, connection);
            if (Params != null)
                for (int i = 0; i < Params.Length; i++)
                    command.Parameters.Add(new MySqlParameter(i.ToString(), Params[i]));
            lock (connection) {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        yield return reader;
                }
            }
            
        }
    }
}
