using System;
using System.Net;
using Newtonsoft.Json.Linq;

namespace request_yt
{
    class ChatRequest
    {
        public static string GetChat(string chid, string api)
        {
            try
            {
                WebClient webClient = new WebClient();
                WebClient n = new WebClient();
                var json = n.DownloadString("https://www.googleapis.com/youtube/v3/liveChat/messages?liveChatId=" + chid + "&part=snippet&key=" + api);
                string valueOriginal = Convert.ToString(json);
                JObject data = JObject.Parse(valueOriginal);
                int lastMsg = Convert.ToInt32(data["pageInfo"]["totalResults"]) - 1;
                if (lastMsg >= 0)
                {
                    string authorId = Convert.ToString(data["items"][lastMsg]["snippet"]["authorChannelId"]);
                    string author = AuthorName(authorId, api);
                    string song = Convert.ToString(data["items"][lastMsg]["snippet"]["textMessageDetails"]["messageText"]);
                    return author + "¤" + song;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0} [ERROR] Cannot get chat info.", DateTime.Now.ToString("H:mm:ss"));
                Console.ResetColor();
                return "";
            }
        }
        private static string AuthorName(string id, string api)
        {
            try
            {
                WebClient webClient = new WebClient();
                WebClient n = new WebClient();
                var json = n.DownloadString("https://www.googleapis.com/youtube/v3/search?part=snippet&maxResults=1&type=channel&channelId=" + id + "&key=" + api);
                string valueOriginal = Convert.ToString(json);
                JObject data = JObject.Parse(valueOriginal);
                return Convert.ToString(data["items"][0]["snippet"]["channelTitle"]);
            }
            catch
            {
                return "[Unknown user]";
            }
        }
    }
}
