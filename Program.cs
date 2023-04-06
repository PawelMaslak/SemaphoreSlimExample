using App.Models;
using App.Service;

//This will limit the number of possible threads to the specified amount.
//It is useful when requesting data from websites/APIs which have certain limitations in place.
SemaphoreSlim gate = new SemaphoreSlim(2); //This will download in parallel maximum two sites.
HttpService service = new HttpService(gate);

var googleUrl = "https://www.google.com";
var bankierUrl = "https://www.bankier.pl/";
var linkedIn = "https://www.linkedin.com/";
var vanguard = "https://www.vanguardinvestor.co.uk/";

string[] urls = new[] { googleUrl, bankierUrl, linkedIn, vanguard };

List<Task<DownloadJob>> jobs = new List<Task<DownloadJob>>();

foreach (var url in urls)
{
    jobs.Add(Task.Run(() => service.GetSiteDataAsyncWithGate(url)));
}

var result = await Task.WhenAll(jobs);

foreach (var downloadJob in result)
{
    Console.WriteLine($"{downloadJob.JobCompleted} | {downloadJob.Url}");
}


