using System;
using System.Collections.ObjectModel;

namespace Models.Processes
{

    public class AppEventsCollection : Collection<AppEvent>
    {


    }


    public class AppEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PriorityEvent : AppEvent
    {
        public int Level { get; set; }

    }
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
