using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using Owin;

using WebApiHost.Middlewares;

namespace WebApiHost
{
    class Program
    {
        static Dictionary<string, Func<Task>> actions = new Dictionary<string, Func<Task>>();

        static void Main(string[] args)
        {

            string baseAddress1 = "http://localhost:9000/";
            

          SetupClients(baseAddress1);
          DefaultHttpServerStartup(baseAddress1);
          

            DoRequests();


        }

        private static void SetupClients(string baseAddress1)
        {
            var client1 = new AnalysisEngineControllerClient(baseAddress1);
            
            actions.Add("1", () => client1.IdentifyRequest());
            
        }

        

        private static void DefaultHttpServerStartup(string baseAddress)
        {
            StartServer(baseAddress, (appBuilder)=> new Startup().Configuration(appBuilder));
        }

        private static void DoRequests()
        {
            Console.WriteLine("Run use cases");

            bool run = true;
            TimeSpan OperationLastTime = new TimeSpan(0);
            while (run)
            {
                Console.Clear();
                Console.WriteLine($"Last operation took {OperationLastTime} to execute");
                Console.WriteLine("1 - Call Identify with default json media formatter");
                 
                Console.WriteLine("3 - Exit");
                var key = Console.ReadKey();
                Func<Task> task = null;
                if (actions.TryGetValue(key.KeyChar.ToString(), out task))
                {
                    try
                    {
                        Stopwatch timeCount = new Stopwatch();
                        timeCount.Start();
                        task();
                        timeCount.Stop();

                        Console.WriteLine($"It took {timeCount.Elapsed} to execute 1");
                        OperationLastTime = timeCount.Elapsed;
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);
                    }

                };

            }
        }

        private static void StartServer(string baseAddress, Action<IAppBuilder> appBuilderHandler)
        {
            //WebApp.Start<Startup>(url: baseAddress);

            WebApp.Start(baseAddress, appBuilderHandler);
        }
    }
}
