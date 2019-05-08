using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Traveler.DAL.DataObjects
{
    [Table("Days")]
    public class Day
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int IdTravel { get; set; }

        public DateTime Date { get; set; }
    }
}
