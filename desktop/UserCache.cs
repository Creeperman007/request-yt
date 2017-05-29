using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace request_yt
{
    class UserCache
    {
        private static SQLiteConnection usernameCache = new SQLiteConnection("Data Source=usernameCache.sqlite; Version=3; New=False");
        public static void CreateCache()
        {
            SQLiteConnection.CreateFile("usernameCache.sqlite");
            usernameCache.Open();
            string sql = "CREATE TABLE usernames (chid VARCHAR(24), name varchar(100))";
            SQLiteCommand command = new SQLiteCommand(sql, usernameCache);
            command.ExecuteNonQuery();
            usernameCache.Close();
        }
        public static string ReadCache(string id)
        {
            try
            {
                usernameCache.Open();
                string sql = "SELECT * FROM `usernames` WHERE chid='" + id + "' ORDER BY chid DESC";
                SQLiteCommand command = new SQLiteCommand(sql, usernameCache);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return Convert.ToString(reader["name"]);
                }
                return "";
            }
            catch
            {
                return "";
            }
            finally
            {
                usernameCache.Close();
            }
        }
        public static void InsertName(string id, string name)
        {
            string check = ReadCache(id);
            if (check == "")
            {
                try
                {
                    usernameCache.Open();
                    string sql = "INSERT INTO `usernames` (chid, name) VALUES ('" + id + "', '" + name + "')";
                    SQLiteCommand command = new SQLiteCommand(sql, usernameCache);
                    command.ExecuteNonQuery();
                    usernameCache.Close();
                }
                catch
                {
                    Console.WriteLine("a");
                }
            }
        }
    }
}
