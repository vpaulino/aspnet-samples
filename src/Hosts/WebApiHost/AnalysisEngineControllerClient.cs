using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApiHost.Models;

namespace WebApiHost
{
    internal class AnalysisEngineControllerClient
    {
        HttpClient client;
        string baseAddress;
        public AnalysisEngineControllerClient(string baseAddress)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.Timeout = new TimeSpan(0,1,0);
            
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.baseAddress = baseAddress;
        }
        public async Task IdentifyRequest()
        {
            Stopwatch timeCount = new Stopwatch();
            timeCount.Start();

            ProcessRequest instance = new ProcessRequest();
            var response = await client.PostAsJsonAsync<ProcessRequest>("identify", instance);
            timeCount.Stop();
            ProcessResult result = null;
             result = await response.Content.ReadAsAsync<ProcessResult>();
            Console.WriteLine($"[ProcessController.IdentifyRequest] - Client {baseAddress} request took {timeCount.Elapsed} ");
            Console.WriteLine($"[ProcessController.IdentifyRequest] - status code {response.StatusCode}, body {result != null} ");

        }
    }
}