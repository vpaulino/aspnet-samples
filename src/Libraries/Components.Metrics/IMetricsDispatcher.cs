using System;
using System.Threading.Tasks;

namespace Components.Web.Http.Middlewares.Logging
{
    public interface IMetricsDispatcher
    {
        Task DispatchMetric<T>(Guid correlationId, string key, T value);
        
    }
}