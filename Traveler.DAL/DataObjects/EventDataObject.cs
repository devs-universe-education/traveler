using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Traveler.DAL.DataObjects
{
    [Table("Events")]
    public class EventDataObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int IdDay { get; set; }

        public string Title { get; set; } = string.Empty;
        //public EventType Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Address { get; set; }
        public bool Remind { get; set; }
        public string Comment { get; set; }
    }
}
