using System;
using System.Collections.Generic;
using System.Text;

namespace Traveler.DAL.DataObjects
{
    public class EventDataObject
    {
        public string Title { get; set; }
        public EventType Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Address { get; set; }
        public bool Remind { get; set; }
        public string Comment { get; set; }
    }

    public enum EventType
    {
        Excursion,
        Eating
            /*
             * need more
             */
    }
}
