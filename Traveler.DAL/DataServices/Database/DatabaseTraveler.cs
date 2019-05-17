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
        
        //--------------------------------------------------------------------------------------------------------------

        public async Task<RequestResult<List<TravelDataObject>>> GetTravelsAsync(CancellationToken ctx)
        {
            var list = await database.Table<TravelDataObject>().ToListAsync();
            return new RequestResult<List<TravelDataObject>>(list, RequestStatus.Ok);
        }

        public async Task<RequestResult> SaveTravelAsync(TravelDataObject item, CancellationToken ctx)
        {
            if (item.Id != 0)
            {
                await database.UpdateAsync(item);              
            }
            else
            {
                await database.RunInTransactionAsync(tran =>
                {
                    var id = tran.Insert(item);

                    for(var date = item.StartDate; date <= item.EndDate; date.AddDays(1))
                    {
                        DayDataObject day = new DayDataObject
                        {
                            IdTravel = id,
                            Date = date
                        };

                        tran.Insert(day);
                    }         
                });             
            }
            return new RequestResult(RequestStatus.Ok);
        }

        public async Task<RequestResult> DeleteTravelAsync(TravelDataObject item, CancellationToken ctx)
        {
            await database.RunInTransactionAsync(tran =>
            {
                var id = tran.Delete(item);
                tran.Execute("DELETE FROM [Days] WHERE [IdTravel] = {0}", id);
            });

            return new RequestResult(RequestStatus.Ok);
        }

        public async Task<RequestResult<List<EventDataObject>>> GetEventAsync(int id, CancellationToken ctx)
        {
            var evnt = await database.QueryAsync<EventDataObject>("SELECT * FROM [Events]" +
                                                                  "WHERE [Id] = {0}", id);

            return new RequestResult<List<EventDataObject>>(evnt, RequestStatus.Ok);
        }

        public async Task<RequestResult<List<EventDataObject>>> GetEventsOfTheDayAsync(int idTrav, DateTime date, CancellationToken ctx)
        {
            var events = await database.QueryAsync<EventDataObject>("SELECT * FROM [Events]" +
                                                                    "WHERE [IdDay] = " +
                                                                    "(SELECT [ID] FROM [Days] " +
                                                                    "WHERE [IdTravel] = {0}  AND [Date] = {1})", idTrav, date);
            return new RequestResult<List<EventDataObject>>(events, RequestStatus.Ok);
        }

        public async Task<RequestResult> SaveEventAsync(EventDataObject item, CancellationToken ctx)
        {
            if (item.Id != 0)
            {
                await database.UpdateAsync(item);
            }
            else
            {
                await database.InsertAsync(item);
            }
            return new RequestResult(RequestStatus.Ok);
        }

        public async Task<RequestResult> DeleteEventAsync(EventDataObject item, CancellationToken ctx)
        {
            await database.DeleteAsync(item);
            return new RequestResult(RequestStatus.Ok);
        }

        public async Task<RequestResult> DeleteEventsByDayAsync(int idDay, CancellationToken ctx)
        {
            await database.ExecuteAsync("DELETE FROM [Events] WHERE [IdDay] = {0}", idDay);
            return new RequestResult(RequestStatus.Ok);
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

        public async Task<RequestResult> DeleteItemsDaysByTravelAsync(int idTravel, CancellationToken ctx)
        {
            await database.ExecuteAsync("DELETE FROM [Days] WHERE [IdTravel] = {0}", idTravel);
            return new RequestResult(RequestStatus.Ok);
        }
    }
}
