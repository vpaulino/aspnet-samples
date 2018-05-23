using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FirstSignalR.Messages
{
     
    public class AppEventsCollection : Collection<AppEvent>
    {
        

    }
     

    public class AppEvent
    {
        public int Id { get; set; }
        public string  Name { get; set; }
    }

    public class PriorityEvent : AppEvent
    {
        public int Level { get; set; }
    }

  
}