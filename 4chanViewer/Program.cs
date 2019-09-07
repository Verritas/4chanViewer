using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;

namespace _4chanViewer
{

    class Program
    {
        static WebClient webClient = new WebClient();
        static JObject json = new JObject();
        static Boards boardsList; // List of boards

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);

        static void Main(string[] args)
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3);

            LoadBoards();
            boardsList = LoadBoards();
            PrintBoards(boardsList);




            //Exit program
            Console.WriteLine("quit...");
            Console.ReadLine();
        }

        public static Boards LoadBoards ()
        {
            dynamic result = webClient.DownloadString("https://a.4cdn.org/boards.json");
            json = JObject.Parse(result);
            return JsonConvert.DeserializeObject<Boards>(result);
        }

        static string boardDescription(string temp)
        {
            int i = 0;

            for (i = 0; i < temp.Length; i++)
            {
                if (temp[i] == 'i' && temp[i + 1] == 's' && temp[i + 2] == ' ') break;
            }

            return temp.Substring(i);
        }

        static void PrintBoards(Boards temp)
        {
            int l = Convert.ToInt32(temp.boardsList.Length.ToString());
            for (int i = 0; i < l; i++)
            {
                Console.WriteLine("{0, -10}\t\t{1, -30}\t\t{2, 35}", temp.boardsList[i].Board, boardsList.boardsList[i].Title, boardDescription(boardsList.boardsList[i].Meta_description.ToString()));
            }
        }
    }
    public class Boards
    {
        [JsonProperty("boards")]
        public boardInformation[] boardsList { get; set; }
    }
    public class boardInformation
    {
        [JsonProperty("board")]
        public string Board { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("ws_board")]
        public int Ws_board { get; set; }
        [JsonProperty("per_page")]
        public int Per_page { get; set; }
        [JsonProperty("pages")]
        public int Pages { get; set; }
        [JsonProperty("max_filesize")]
        public int Max_filesize { get; set; }
        [JsonProperty("max_webm_filesize")]
        public int Max_webm_filesize { get; set; }
        [JsonProperty("max_comment_chairs")]
        public int Max_comment_chairs { get; set; }
        [JsonProperty("max_webm_duration")]
        public int Max_webm_duration { get; set; }
        [JsonProperty("bump_limit")]
        public int Bump_limit { get; set; }
        [JsonProperty("image_limit")]
        public int Image_limit { get; set; }
        [JsonProperty("cooldowns")]
        public Cooldowns Cooldowns { get; set; }
        [JsonProperty("meta_description")]
        public string Meta_description { get; set; }
        [JsonProperty("is_archived")]
        public int Is_archived { get; set; }
        [JsonProperty("webm_audio")]
        public int Webm_audio { get; set; }
    }

    public class Cooldowns
    {
        [JsonProperty("threads")]
        public int Threads { get; set; }
        [JsonProperty("replies")]
        public int Replies { get; set; }
        [JsonProperty("images")]
        public int Images { get; set; }
    }
}
