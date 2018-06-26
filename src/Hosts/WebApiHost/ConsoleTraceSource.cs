using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebApiHost
{
    public interface IVBTraceSource
    {
        bool ShouldTrace(TraceEventType eventType);
        
        void TraceEvent(TraceEventType eventType, string id, string message);
        
    }
    public class ConsoleTraceSource : IVBTraceSource
    {
        public bool ShouldTrace(TraceEventType eventType)
        {
            return true;
        }

        public void TraceEvent(TraceEventType eventType, string id, string message)
        {
            Console.WriteLine($"{eventType} - {id} : {message}");
        }
    }
}
