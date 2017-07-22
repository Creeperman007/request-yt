using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace request_yt
{
    class Program
    {
        public static string v = "1.3.1";
        public static bool open = true;
        public static string request;
        public static string requestLast;
        public static Random r = new Random();
        public static int session = r.Next(1, 10000);
        public static string autoupdate;
        static void Main(string[] args)
        {
            if (File.Exists("temp/RequestYT-Setup.exe"))
            {
                try
                {
                    File.Delete("temp/RequestYT-Setup.exe");
                }
                catch { }
            }
            DBConnect db = new DBConnect();
            var conf = new Config.IniFile("config.ini");
            var chid = "";
            var api = "";
            int refresh = 500;
            if (!File.Exists("usernameCache.sqlite"))
            {
                UserCache.CreateCache();
            }
            if (!File.Exists("config.ini"))
            {
                conf.Write("RefreshTime", "", "Settings");
                conf.Write("DBHost", "", "SQL");
                conf.Write("DBUser", "", "SQL");
                conf.Write("DBPass", "", "SQL");
                conf.Write("DBName", "", "SQL");
                conf.Write("ChatID", "", "YTChat");
                conf.Write("APIKey", "", "YTChat");
                conf.Write("AutoUpdate", "true", "Updater");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0} [ERROR] Cannot find config.ini. Creating new one. Please fill the config in.", DateTime.Now.ToString("H:mm:ss"));
                Console.ResetColor();
                open = false;
                goto skip;
            }
            try
            {
                chid = conf.Read("ChatID", "YTChat");
                api = conf.Read("APIKey", "YTChat");
                refresh = Convert.ToInt32(conf.Read("RefreshTime", "Settings"));
                try
                {
                    autoupdate = conf.Read("AutoUpdate", "Updater");
                }
                catch
                {
                    autoupdate = "false";
                    try
                    {
                        conf.Write("AutoUpdate", "false", "Updater");
                    }
                    catch { }
                }
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0} [ERROR] Cannot read variables from config. Did you fill in the config?", DateTime.Now.ToString("H:mm:ss"));
                Console.ResetColor();
                open = false;
                goto skip;
            }
            Console.WriteLine("{1} [INFO] Request system v{0}\n{1} [INFO] Programmed by Creeperman007", v, DateTime.Now.ToString("H:mm:ss"));
            Thread.Sleep(250);
            Version.Get(v);
            Thread.Sleep(500);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{1} [INFO] Session key: {0}\n{1} [INFO] Key is used for accessing the web interface.", session, DateTime.Now.ToString("H:mm:ss"));
            Console.ResetColor();
            request = "Test¤Testing connection.";
            Console.WriteLine("{0} [TEST] Testing connection with database", DateTime.Now.ToString("H:mm:ss"));
            db.DBConnector(session, request);
            Console.WriteLine("{0} [TEST] Ending testing session", DateTime.Now.ToString("H:mm:ss"));
        skip:
            while (open == true)
            {
                request = ChatRequest.GetChat(chid, api);
                if (Compare(request) && request != requestLast && !request.Contains("SongName") && request.Contains(' '))
                {
                    Console.WriteLine("{0} [INFO] Adding new request", DateTime.Now.ToString("H:mm:ss"));
                    try
                    {
                        db.DBConnector(session, request);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("{1} [ERROR] An error occurred:\n{0}", ex.ToString(), DateTime.Now.ToString("H:mm:ss"));
                        Console.ResetColor();
                        MessageBox.Show("An error occurred!\nMore info in console.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        open = false;
                    }
                }
                requestLast = request;
                request = "";
                Thread.Sleep(refresh);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        private static bool Compare(string input)
        {
            string[] commands = { "!request", "!Request", "!r", "!R" };
            foreach (string compare in commands)
            {
                if (input.Contains(compare))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
