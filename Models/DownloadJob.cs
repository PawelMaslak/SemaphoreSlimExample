using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Models
{
    public record DownloadJob
    {
        public string Url { get; set; }
        public bool JobCompleted { get; set; }
        public string SiteContent { get; set; }

        public DownloadJob(string url, string siteContent, bool jobCompleted)
        {
            Url = url;
            SiteContent = siteContent;
            JobCompleted = jobCompleted;
        }
    }
}
