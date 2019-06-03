using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SQLite;
using Traveler.DAL.DataObjects;


namespace Traveler.DAL.DataServices.Database
{
    public class DatabaseTraveler : ITravelerDataService
    {
        readonly SQLiteAsyncConnection database;

        public DatabaseTraveler(string connectionstring)
        {
            database = new SQLiteAsyncConnection(connectionstring);

            database.CreateTablesAsync<TravelDataObject, DayDataObject, EventDataObject>();
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

        public async Task<RequestResult<List<TravelDataObject>>> GetTravelsOfMonthAsync(DateTime date, CancellationToken ctx)
        {
            try
            {
                DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                var list = await database.QueryAsync<TravelDataObject>("SELECT * FROM [Travels]" +
                                                                       "WHERE ([StartDate] BETWEEN ? AND ?) OR ([EndDate] BETWEEN ? AND ?)",
                                                                       firstDayOfMonth, lastDayOfMonth, firstDayOfMonth, lastDayOfMonth);

                return new RequestResult<List<TravelDataObject>>(list, RequestStatus.Ok);
            }
            catch (Exception)
            {
                return new RequestResult<List<TravelDataObject>>(null, RequestStatus.DatabaseError);
            }
        }

        public async Task<RequestResult> SaveTravelAsync(TravelDataObject item, CancellationToken ctx)
        {
            if (item == null)
                return new RequestResult(RequestStatus.InvalidRequest);

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
            if (item == null)
                return new RequestResult(RequestStatus.InvalidRequest);

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

        public async Task<RequestResult<DayDataObject>> GetDayAsync(int idTravel, DateTime day, CancellationToken ctx)
        {
            if (idTravel <= 0)
                return new RequestResult<DayDataObject>(null, RequestStatus.InvalidRequest);

            try
            {
                var dayObject = await database.Table<DayDataObject>().Where(x => x.IdTravel == idTravel && x.Date == day).FirstOrDefaultAsync();
                return new RequestResult<DayDataObject>(dayObject, RequestStatus.Ok);
            }
            catch (Exception)
            {
                return new RequestResult<DayDataObject>(null, RequestStatus.DatabaseError);
            }
        }

        public async Task<RequestResult<List<EventDataObject>>> GetEventsOfDayAsync(int idTravel, DateTime day, CancellationToken ctx)
        {
            if (idTravel <= 0)
                return new RequestResult<List<EventDataObject>>(null, RequestStatus.InvalidRequest);

            try
            {
                var events = await database.QueryAsync<EventDataObject>("SELECT * FROM [Events] WHERE [IdDay] = " +
                                                                        "(SELECT [ID] FROM [Days] WHERE [IdTravel] = ? AND [Date] = ?)", idTravel, day);

                return new RequestResult<List<EventDataObject>>(events, RequestStatus.Ok);
            }
            catch (Exception)
            {
                return new RequestResult<List<EventDataObject>>(null, RequestStatus.DatabaseError);
            }
        }

        public async Task<RequestResult<List<EventDataObject>>> GetEventsOfCurrentDayAsync(DateTime today, CancellationToken ctx)
        {
            try
            {
                var events = await database.QueryAsync<EventDataObject>("SELECT * FROM [Events] WHERE [IdDay] = " +
                                                                        "(SELECT [ID] FROM [Days] WHERE [Date] = ?)", today);

                return new RequestResult<List<EventDataObject>>(events, RequestStatus.Ok);
            }
            catch (Exception)
            {
                return new RequestResult<List<EventDataObject>>(null, RequestStatus.DatabaseError);
            }
        }

        public async Task<RequestResult> SaveEventAsync(EventDataObject item, CancellationToken ctx)
        {
            if (item == null)
                return new RequestResult(RequestStatus.InvalidRequest);

            try
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
            catch (Exception)
            {
                return new RequestResult(RequestStatus.DatabaseError);
            }
        }

        public async Task<RequestResult> DeleteEventAsync(EventDataObject item, CancellationToken ctx)
        {
            if (item == null)
                return new RequestResult(RequestStatus.InvalidRequest);

            try
            {
                await database.DeleteAsync(item);
                return new RequestResult(RequestStatus.Ok);
            }
            catch (Exception)
            {
                return new RequestResult(RequestStatus.DatabaseError);
            }
        }        
    }
}
