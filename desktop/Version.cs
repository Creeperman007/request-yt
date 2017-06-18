using System;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Threading;

namespace request_yt
{
    class Version
    {
        public static void Get(string v)
        {
            try
            {
                WebClient n = new WebClient();
                var json = n.DownloadString("http://apis.creeperman007.tk/request-yt/v1/");
                string valueOriginal = Convert.ToString(json);
                JObject data = JObject.Parse(valueOriginal);
                string version = Convert.ToString(data["latestVersion"]);
                if (v == version)
                {
                    Console.WriteLine("{0} [INFO] You have latest version.", DateTime.Now.ToString("H:mm:ss"));
                }
                else
                {
                    if (Program.autoupdate == "true")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("{0} [WARN] New version is available.", DateTime.Now.ToString("H:mm:ss"));
                        Console.ResetColor();
                        Console.WriteLine("{0} [INFO] Starting updater...", DateTime.Now.ToString("H:mm:ss"));
                        try
                        {
                            Process.Start("app-updater.exe");
                            Environment.Exit(0);
                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("{0} [ERROR] Cannot start updater. Please, consider manual update.", DateTime.Now.ToString("H:mm:ss"));
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("{0} [WARN] New version is available. Please, consider updating.", DateTime.Now.ToString("H:mm:ss"));
                        Console.ResetColor();
                    }
                }
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0} [ERROR] Cannot get info about latest version.", DateTime.Now.ToString("H:mm:ss"));
                Console.ResetColor();
            }
        }
    }
}
