using System;
using System.Collections.Generic;
using System.Text;

namespace Traveler.DAL.DataObjects
{
    public class TravelDataObject : List<DayDataObject>
    {
        public string Title { get; set; }
    }
}
