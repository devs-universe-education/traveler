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

        public Task<List<Travel>> GetItemsTravelAsync()
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
            return database.QueryAsync<Event>("SELECT * FROM [Events]" +
                "WHERE [IdDay] = " +
                "(SELECT [ID] FROM [Days] " +
                "WHERE [IdTravel] = {0}  AND [Date] = {1})", IdTrav, date);
        }

        public Task<int> SaveItemTravelAsync(Travel item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> SaveItemEventAsync(Event item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }


        public Task<int> DeleteItemTravelAsync(Travel item)
        {
            return database.DeleteAsync(item);
        }

        public Task<int> DeleteItemEventAsync(Event item)
        {
            return database.DeleteAsync(item);
        }

    }
}
