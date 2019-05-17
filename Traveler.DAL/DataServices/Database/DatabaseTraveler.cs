using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SQLite;
using Traveler.DAL.DataObjects;
using Xamarin.Forms;

namespace Traveler.DAL.DataServices.Database
{
    public class DatabaseTraveler : ITravelerDataService
    {
        readonly SQLiteAsyncConnection database;
       
        public DatabaseTraveler()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();

            database.CreateTableAsync<TravelDataObject>().Wait();
            database.CreateTableAsync<DayDataObject>().Wait();
            database.CreateTableAsync<EventDataObject>().Wait();
        }

        public Task<List<TravelDataObject>> GetTraveslAsync()
        {
            return database.Table<TravelDataObject>().ToListAsync();
        }

        public Task<List<DayDataObject>> GetItemsDayAsync()
        {
            return database.Table<DayDataObject>().ToListAsync();
        }

        public Task<List<EventDataObject>> GetItemsEventAsync()
        {
            return database.Table<EventDataObject>().ToListAsync();
        }
        
        public Task<List<EventDataObject>> GetItemsEventsOfTheDayAsync(int idTrav, DateTime date)
        {
            return database.QueryAsync<EventDataObject>("SELECT * FROM [Events]" +
                "WHERE [IdDay] = " +
                "(SELECT [ID] FROM [Days] " +
                "WHERE [IdTravel] = {0}  AND [Date] = {1})", idTrav, date);
        }

        public Task<List<EventDataObject>> GetEventAsync(int id)
        {
            return database.QueryAsync<EventDataObject>("SELECT * FROM [Events]" +
                                              "WHERE [Id] = {0}", id);
        }

        public Task<int> SaveItemTravelAsync(TravelDataObject item)
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

        public Task<int> SaveItemDayAsync(DayDataObject item)
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

        public Task<int> SaveItemEventAsync(EventDataObject item)
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

        public Task<int> DeleteItemTravelAsync(TravelDataObject item)
        {
            return database.DeleteAsync(item);
        }

        public Task<int> DeleteItemEventAsync(EventDataObject item)
        {
            return database.DeleteAsync(item);
        }

        public Task<List<DayDataObject>> DeleteItemsDaysByTravelAsync(int idTravel)
        {
            return database.QueryAsync<DayDataObject>("DELETE FROM [Day] WHERE [IdTravel] = {0}", idTravel);
        }

        public Task<List<EventDataObject>> DeleteItemsEventsByDayAsync(int idDay)
        {
            return database.QueryAsync<EventDataObject>("DELETE FROM [Event] WHERE [IdDayl] = {0}", idDay);
        }

        //--------------------------------------------------------------------------------------------------------------

        public Task<RequestResult<List<TravelDataObject>>> GetTravelsAsync(CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult> SaveTravelAsync(TravelDataObject item, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult> DeleteTravelAsync(TravelDataObject item, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult<List<EventDataObject>>> GetEventAsync(int id, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult<List<EventDataObject>>> GetEventsOfTheDayAsync(int idTrav, DateTime date, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult> SaveEventAsync(EventDataObject item, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult> DeleteEventAsync(EventDataObject item, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult> DeleteEventsByDayAsync(int idDay, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult<DayDataObject>> GetDayDataObject(CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult<EventDataObject>> GetEventDataObject(CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult<TravelDataObject>> GetTravelDataObject(CancellationToken ctx)
        {
            throw new NotImplementedException();
        }
    }
}
