using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using App.Models;

namespace App.Service
{
    public class HttpService
    {
        private readonly SemaphoreSlim _gate;
        private readonly HttpClient _client;

        public HttpService(SemaphoreSlim gate)
        {
            _gate = gate;
            _client = new HttpClient();
        }

        public async Task<DownloadJob> GetSiteDataAsync(string url)
        {
            try
            {
                Console.WriteLine($"Received the request to download site: {url}");
                Console.WriteLine($"Downloading the site: {url}");
                var siteData = await _client.GetStringAsync(url);
                Thread.Sleep(2000); //mocking longer operation...
                Console.WriteLine($"Site downloaded!");
                return new DownloadJob(url, siteData, true);
            }
            catch (Exception)
            {
                return new DownloadJob(url, string.Empty, false);
            }
        }

        public async Task<DownloadJob> GetSiteDataAsyncWithGate(string url)
        {
            try
            {
                Console.WriteLine($"Received the request to download site: {url}");
                await _gate.WaitAsync();
                Console.WriteLine($"Downloading the site: {url}");
                var siteData = await _client.GetStringAsync(url);
                Thread.Sleep(2000); //mocking longer operation...
                Console.WriteLine($"Site downloaded!");
                _gate.Release();
                return new DownloadJob(url, siteData, true);
            }
            catch (Exception)
            {
                return new DownloadJob(url, string.Empty, false);
            }
        }
    }
}
