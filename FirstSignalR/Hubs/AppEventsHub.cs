using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstSignalR.Messages;
using Microsoft.AspNetCore.SignalR;

namespace FirstSignalR
{
    public class AppEventsHub : Hub
    {
        
        public async Task StartProcessMonitor(int processId)
        {

            ProcessDetail processDetail = new ProcessDetail()
            {
                Id = processId,
                Name = "Bart Process",
                Events = new AppEventsCollection() { new AppEvent() { Id = 2, Name = "riding skate" }, new PriorityEvent() { Id = 1, Level = 2, Name ="playing with milhouse" } }
            };

            
           await Clients.All.SendAsync("ReceiveEvent", processDetail);
        }

       
    }
}
