using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Algonia
{
    public class ImageDownloaderAlgo
    {
        public enum URLS_SOURCE { FILE, JSON }
        List<DownloadStatus> _statuses = new List<DownloadStatus>();
        string _urlsSource;
        string _destinationPath;
        URLS_SOURCE _sourceType;

        public ImageDownloaderAlgo(string urlsSource, string destinationPath, URLS_SOURCE sourceType)
        {
            this._urlsSource = urlsSource;
            this._destinationPath = destinationPath;
            this._sourceType = sourceType;
        }

        public string Download()
        {
            if (Directory.Exists(Path.Combine(Environment.CurrentDirectory, _destinationPath)))
            {
                Console.WriteLine(string.Format("Clearing {0} dir...", Path.Combine(Environment.CurrentDirectory, _destinationPath)));
                Directory.Delete(Path.Combine(Environment.CurrentDirectory, _destinationPath), true);
            }

            Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, _destinationPath));

            switch (_sourceType)
            {
                case URLS_SOURCE.FILE:
                    
                    foreach (var url in File.ReadAllLines(_urlsSource))
                        DownloadImage(url);
                    break;

                case URLS_SOURCE.JSON:

                    foreach(var url in JsonConvert.DeserializeObject<List<UrlModel>>(_urlsSource))
                        DownloadImage(url.Url);
                    break;
            }
            return System.Text.Json.JsonSerializer.Serialize<List<DownloadStatus>>(_statuses);
        }

        void DownloadImage(string url)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    Console.WriteLine(string.Format("Downloading {0}", url));
                    client.DownloadFile(new Uri(url), Path.Combine(Environment.CurrentDirectory, _destinationPath, Path.GetFileName(new Uri(url).LocalPath)));
                    _statuses.Add(new DownloadStatus(url, 200));
                }
                catch (WebException webEx)
                {
                    _statuses.Add(new DownloadStatus(url, (int)((HttpWebResponse)webEx.Response).StatusCode));
                }
                catch
                {
                    _statuses.Add(new DownloadStatus(url, 500));
                }
            }
        }
    }
}
