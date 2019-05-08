using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Traveler.DAL.DataObjects;
using Xamarin.Forms;

namespace Traveler.DAL.dbSQLite
{
    public class DatabaseTraveler
    {
        private SQLiteAsyncConnection database;
       
        public DatabaseTraveler()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();

            database.CreateTableAsync<Travel>().Wait();
            database.CreateTableAsync<Day>().Wait();
            database.CreateTableAsync<Event>().Wait();
        }

        public Task<List<Travel>> GetItemTravelAync()
        {
            return database.Table<Travel>().ToListAsync();
        }
    }
}
