using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace BassRebels
{
    class Program
    {
        public static string v = "1.0";
        public static bool open = true;
        public static string request;
        public static string requestLast;
        public static Random r = new Random();
        public static int session = r.Next(1, 10000);
        static void Main(string[] args)
        {
            DBConnect db = new DBConnect();
            var conf = new Config.IniFile("config.ini");
            var chid = "";
            var api = "";
            int refresh = 0;
            if (!File.Exists("config.ini"))
            {
                conf.Write("RefreshTime", "");
                conf.Write("DBHost", "", "SQL");
                conf.Write("DBUser", "", "SQL");
                conf.Write("DBPass", "", "SQL");
                conf.Write("DBName", "", "SQL");
                conf.Write("ChatID", "", "YTChat");
                conf.Write("APIKey", "", "YTChat");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0} [ERROR] Cannot find config.ini. Creating new one. Please fill the config in.", DateTime.Now.ToString("H:mm:ss"));
                Console.ResetColor();
                open = false;
                goto skip;
            }
            chid = conf.Read("ChatID", "YTChat");
            api = conf.Read("APIKey", "YTChat");
            refresh = Convert.ToInt32(conf.Read("RefreshTime", "Settings"));
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
                if ((request.Contains("!request") || request.Contains("!r") || request.Contains("!Request")) && request != requestLast && !request.Contains("SongName") && request.Contains(' '))
                {
                    Console.WriteLine("{0} [INFO] Adding new request", DateTime.Now.ToString("H:mm:ss"));
                    try
                    {
                        db.DBConnector(session, request);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("{1} [ERROR] An error occurred:\n{0}\nPress any key to continue...", ex.ToString(), DateTime.Now.ToString("H:mm:ss"));
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
    }
}
