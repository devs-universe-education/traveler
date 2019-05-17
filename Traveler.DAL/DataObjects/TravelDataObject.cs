using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Traveler.DAL.DataObjects
{
    [Table("Travels")]
    public class TravelDataObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
