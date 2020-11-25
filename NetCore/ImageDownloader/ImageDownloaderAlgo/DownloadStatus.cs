using System;
using System.Collections.Generic;
using System.Text;

public class DownloadStatus
{
    public string Url { get; set; }
    public int Status { get; set; }
    public DownloadStatus(string Url, int Status)
    {
        this.Status = Status;
        this.Url = Url;
    }
}

