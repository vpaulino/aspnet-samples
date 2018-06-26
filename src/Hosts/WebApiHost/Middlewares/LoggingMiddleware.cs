using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiHost.Middlewares
{
    public class LoggingMiddleware

    {

        private System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task> appFunc;

        public LoggingMiddleware(System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task> func)

        {

            this.appFunc = func;

        }

        public async Task Invoke(IDictionary<string, object> env)

        {
            Stopwatch clock = new Stopwatch();
            clock.Start();
            Console.WriteLine("Logging middleware Starting ");
            try
            {
                await appFunc.Invoke(env);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Logging middleware: ex:{ex}");
            }
            finally
            {
                clock.Stop();
                Console.WriteLine($"Logging middleware Ended: {clock.Elapsed}");
            }
         

            
        }

    }
}
