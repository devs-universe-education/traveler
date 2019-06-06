using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Traveler.DAL.DataObjects;

namespace Traveler.DAL.DataServices.Mock
{
    class MockTravelerDataService : BaseMockDataService, ITravelerDataService
    {
        public Task<RequestResult<List<TravelDataObject>>> GetTravelsAsync(CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult<List<TravelDataObject>>> GetTravelsOfMonthAsync(DateTime date, CancellationToken ctx)
        {
            return GetMockDataList<TravelDataObject>("Traveler.DAL.Resources.Mock.Main.travels.json");
        }

        public Task<RequestResult> SaveTravelAsync(TravelDataObject item, CancellationToken ctx)
        {
            return Task.FromResult(new RequestResult(RequestStatus.Ok));
        }

        public Task<RequestResult> DeleteTravelAsync(TravelDataObject item, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult<DayDataObject>> GetDayAsync(int idTravel, DateTime day, CancellationToken ctx)
        {
            return GetMockData<DayDataObject>("Traveler.DAL.Resources.Mock.Main.day.json");
        }

        public Task<RequestResult<List<EventDataObject>>> GetEventsOfDayAsync(int idTravel, DateTime day, CancellationToken ctx)
        {
            return GetMockDataList<EventDataObject>("Traveler.DAL.Resources.Mock.Main.events.json");
        }

        public Task<RequestResult<List<EventDataObject>>> GetEventsOfCurrentDayAsync(DateTime today, CancellationToken ctx)
        {
            return GetMockDataList<EventDataObject>("Traveler.DAL.Resources.Mock.Main.events.json");
        }

        public Task<RequestResult> SaveEventAsync(EventDataObject item, CancellationToken ctx)
        {
            return Task.FromResult(new RequestResult(RequestStatus.InvalidRequest));
        }

        public Task<RequestResult> DeleteEventAsync(EventDataObject item, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult<string>> GetEventTitleAsync(DateTime startTime)
        {
            return Task.FromResult(new RequestResult<string>(null, RequestStatus.DatabaseError));
        }
    }
}
