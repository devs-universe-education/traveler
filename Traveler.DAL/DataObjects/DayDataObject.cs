using System;
using System.Collections.Generic;
using System.Text;

namespace Traveler.DAL.DataObjects
{
    class DayDataObject : List<EventDataObject>
    {
        public DateTime Date { get; set; }
    }
}
