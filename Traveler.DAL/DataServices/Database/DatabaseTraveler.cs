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

        public async Task<RequestResult<List<TravelDataObject>>> GetTravelsAsync(CancellationToken ctx)
        {
            try
            {
                var list = await database.Table<TravelDataObject>().ToListAsync();
                return new RequestResult<List<TravelDataObject>>(list, RequestStatus.Ok);
            }
            catch (Exception)
            {
                return new RequestResult<List<TravelDataObject>>(null, RequestStatus.DatabaseError);
            }
        }

        public async Task<RequestResult> SaveTravelAsync(TravelDataObject item, CancellationToken ctx)
        {
            try
            {
                if (item.Id != 0)
                {
                    //await database.UpdateAsync(item);
                }
                else
                {
                    await database.RunInTransactionAsync(tran =>
                    {
                        tran.Insert(item); //add travel

                        for (DateTime date = item.StartDate; date <= item.EndDate; date = date.AddDays(1.0))
                        {
                            DayDataObject day = new DayDataObject() { IdTravel = item.Id, Date = date };
                            tran.Insert(day); //add day
                        }
                    });
                }

                return new RequestResult(RequestStatus.Ok);
            }
            catch (Exception)
            {
                return new RequestResult(RequestStatus.DatabaseError);
            }
        }

        public async Task<RequestResult> DeleteTravelAsync(TravelDataObject item, CancellationToken ctx)
        {
            try
            {
                await database.RunInTransactionAsync(tran =>
                {
                    tran.Delete(item); //delete travel

                    var days = tran.Query<DayDataObject>("SELECT * FROM [Days] WHERE [IdTravel] = ?", item.Id); //get days of travel
                    foreach (var day in days)
                    {
                        tran.Execute("DELETE FROM [Events] WHERE [IdDay] = ?", day.Id); //delete events of day
                    }

                    tran.Execute("DELETE FROM [Days] WHERE [IdTravel] = ?", item.Id); //delete days of travel
                });

                return new RequestResult(RequestStatus.Ok);
            }
            catch (Exception)
            {
                return new RequestResult(RequestStatus.DatabaseError);
            }
        }

        public async Task<RequestResult<List<EventDataObject>>> GetEventAsync(int id, CancellationToken ctx)
        {
            var evnt = await database.QueryAsync<EventDataObject>("SELECT * FROM [Events]" +
                                                                  "WHERE [Id] = ?", id);

            return new RequestResult<List<EventDataObject>>(evnt, RequestStatus.Ok);
        }

        public async Task<RequestResult<List<EventDataObject>>> GetEventsOfTheDayAsync(int idTrav, DateTime date, CancellationToken ctx)
        {
            var events = await database.QueryAsync<EventDataObject>("SELECT * FROM [Events]" +
                                                                    "WHERE [IdDay] = " +
                                                                    "(SELECT [ID] FROM [Days] " +
                                                                    "WHERE [IdTravel] = ?  AND [Date] = ?)", idTrav, date);
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
            await database.ExecuteAsync("DELETE FROM [Events] WHERE [IdDay] = ?", idDay);
            return new RequestResult(RequestStatus.Ok);
        }
    }
}
