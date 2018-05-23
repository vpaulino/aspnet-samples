using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FirstSignalR.Messages
{

    public class Process
    {
        public int Id { get; set; }

        public string Name { get; set; }


    }

    public class ProcessDetail : Process
    {
        
        public AppEventsCollection Events { get; set; }

    }


}
