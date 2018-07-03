#if NET462
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

namespace Components.Web.Http.Middlewares.Logging
{

    public static class MiddlewareExtensions
    {
        public static IAppBuilder UseMetricsMiddleware(this IAppBuilder app)
        {
            app.Use<MetricsMiddleware>();
            return app;
        }
    }

    public class MetricsMiddleware : OwinMiddleware
    { 
        OwinMiddleware next;
        readonly IMetricsDispatcher metricsDispatcher;
        public MetricsMiddleware(OwinMiddleware next, IMetricsDispatcher metricsDispatcher) : base(next)
        {

            this.next = next;
            this.metricsDispatcher = metricsDispatcher;

        }

        private async Task RecordMetrics(Guid requestId, IOwinRequest request)
        {
            await metricsDispatcher.DispatchMetric(requestId, "countRequests",  1);
            await metricsDispatcher.DispatchMetric(requestId, "Server", request.Host);
            await metricsDispatcher.DispatchMetric(requestId, "Path", request.Path);
            await metricsDispatcher.DispatchMetric(requestId, "IsSecure", request.IsSecure);
            await metricsDispatcher.DispatchMetric(requestId, "HttpMethod", request.Method);
            await metricsDispatcher.DispatchMetric(requestId, "QueryString", request.QueryString);
            await metricsDispatcher.DispatchMetric(requestId, "RequestContentType", request.ContentType);
            await metricsDispatcher.DispatchMetric(requestId, "Accept", request.Accept);
        }

        private async Task RecordMetrics(Guid requestId,  IOwinResponse response)
        {
            await metricsDispatcher.DispatchMetric(requestId, "StatusCode",  response.StatusCode);
            await metricsDispatcher.DispatchMetric(requestId, "ContentLength", response.ContentLength);
            await metricsDispatcher.DispatchMetric(requestId, "ResponseContentType",  response.ContentType);
        }


        public override async Task Invoke(IOwinContext context)
        {

            Stopwatch clock = new Stopwatch();
            clock.Start();
            await context.TraceOutput.WriteLineAsync("Logging middleware Starting");
            Guid requestId = Guid.NewGuid();
            try
            {
               await RecordMetrics(requestId, context.Request);
               await this.next.Invoke(context);
               await RecordMetrics(requestId, context.Response);
            

            }
            catch (Exception ex)
            {
                await context.TraceOutput.WriteAsync($"Logging MetricsMiddleware: ex:{ex}");
                
            }
            finally
            {
                clock.Stop();
                await metricsDispatcher.DispatchMetric(requestId, "ExecutionTime", clock.Elapsed);
            }
        }

         

    }
}
#endif