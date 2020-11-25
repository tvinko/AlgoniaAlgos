using Algonia;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace ImageDownloadDebug
{
    public class Program
    {
        static void Main(string[] args)
        {
            ImageDownloaderAlgo algo = new ImageDownloaderAlgo("urls.txt", "tmp", ImageDownloaderAlgo.URLS_SOURCE.FILE);

            foreach (var j in JArray.Parse(algo.Download()))
                Console.WriteLine(j["Url"].ToString());
        }
    }
}
