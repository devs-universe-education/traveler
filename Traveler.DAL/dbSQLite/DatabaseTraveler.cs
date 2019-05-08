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

        public Task<List<Travel>> GetItemsTravelAync()
        {
            return database.Table<Travel>().ToListAsync();
        }

        public Task<List<Day>> GetItemsDayAsync()
        {
            return database.Table<Day>().ToListAsync();
        }

        public Task<List<Event>> GetItemsEventAsync()
        {
            return database.Table<Event>().ToListAsync();
        }

        public Task<List<Event>> GetEventsOfTheDayAsync(int IdTrav, DateTime date)
        {
            var IdDay = database.QueryAsync<Day>("SELECT [ID] FROM [Days] " +
                "WHERE [IdTravel] = {0}  AND [Date] = {1}", IdTrav, date);

            return database.QueryAsync<Event>("SELECT * FROM [Events]" +
                "WHERE [IdDay] = {0}", IdDay);
        }
    }
}
