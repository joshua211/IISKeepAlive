using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace IISKeepAlive
{
    class Program
    {
        static string path = "./Urls.json";
        static int interval = 3;
        static bool logResults = false;
        static bool usePing = false;
        static string logPath = "./log.log";

        static async Task Main(string[] args)
        {
            ParseArguments(args);
            var client = new RestClient();
            do
            {
                var json = File.ReadAllText(path);
                var urls = JsonConvert.DeserializeObject<IEnumerable<string>>(json);
                foreach (var url in urls)
                {
                    var request = new RestRequest(url, Method.GET);
                    var response = await client.ExecuteAsync(request);
                    if (logResults)
                    {
                        var succes = response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.Forbidden;
                        var time = DateTime.Now;

                        File.AppendAllText(logPath, $"-{time.ToString("dd/MM/yy HH:mm:ss")}: Rest request to {url} {(succes ? "succeeded" : "failed")} \n");
                    }
                }
                await Task.Delay(new TimeSpan(0, interval, 0));
            } while (true);
        }

        private static void ParseArguments(string[] args)
        {
            foreach (var arg in args)
            {
                if (!arg.StartsWith('-'))
                    continue;

                var split = arg.Trim('-').Split('=');
                if (split.Length < 2)
                    continue;

                switch (split[0].ToLower())
                {
                    case "path":
                        path = split[1];
                        break;
                    case "interval":
                        interval = Int32.Parse(split[1]);
                        break;
                    case "logresults":
                        logResults = Boolean.Parse(split[1]);
                        break;
                    case "useping":
                        usePing = Boolean.Parse(split[1]);
                        break;
                    case "logpath":
                        logPath = split[1];
                        break;
                }
            }
        }
    }
}
