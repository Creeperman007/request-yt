using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace request_yt
{
    class DBConnect
    {
        public void DBConnector(int idIns, string[] reqIns)
        {
            Console.WriteLine("{0} [INFO] Connecting to database", DateTime.Now.ToString("H:mm:ss"));
            var conf = new Config.IniFile("config.ini");
            var host = "";
            var user = "";
            var pass = "";
            var name = "";
            try
            {
                host = conf.Read("DBHost", "SQL");
                user = conf.Read("DBUser", "SQL");
                pass = conf.Read("DBPass", "SQL");
                name = conf.Read("DBName", "SQL");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0} [ERROR] Cannot read variables (SQL) from config. Did you fill in the config?", DateTime.Now.ToString("H:mm:ss"));
                Console.ResetColor();
            }
            string cs = @"server=" + host + ";userid=" + user + ";password=" + pass + ";database=" + name;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO `requests` (session, name, author, state) VALUES (@Session, @Name, @Author, 'active')";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@Name", reqIns[1]);
                cmd.Parameters.AddWithValue("@Author", reqIns[0]);
                cmd.Parameters.AddWithValue("@Session", idIns);
                cmd.ExecuteNonQuery();
                Console.WriteLine("{0} [INFO] Successfully added request to database", DateTime.Now.ToString("H:mm:ss"));

            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 1042:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("{1} [ERROR] Cannot connect to database server:\n{0}", ex.ToString(), DateTime.Now.ToString("H:mm:ss"));
                        Console.ResetColor();
                        MessageBox.Show("Cannot connect to database server!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Program.open = false;
                        break;
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("{1} [ERROR] Invalid username/password:\n{0}", ex.ToString(), DateTime.Now.ToString("H:mm:ss"));
                        Console.ResetColor();
                        MessageBox.Show("Authentication to host failed: Access denied.\nPlease, check the config.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Program.open = false;
                        break;
                    case 1366:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("{1} [ERROR] Cannot add request to the database\n", ex.Number, DateTime.Now.ToString("H:mm:ss"));
                        Console.ResetColor();
                        Program.open = true;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("{2} [ERROR] An error {1} occurred:\n{0}", ex.ToString(), ex.Number, DateTime.Now.ToString("H:mm:ss"));
                        Console.ResetColor();
                        MessageBox.Show("An error occurred!\nMore info in console.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Program.open = false;
                        break;
                }
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}