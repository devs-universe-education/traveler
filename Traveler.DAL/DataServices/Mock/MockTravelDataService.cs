using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Traveler.DAL.DataObjects;

namespace Traveler.DAL.DataServices.Mock
{
    class MockTravelDataService : BaseMockDataService, ITravelerDataService
    {
        public Task<RequestResult> DeleteEventAsync(EventDataObject item, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult> DeleteEventsByDayAsync(int idDay, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult> DeleteTravelAsync(TravelDataObject item, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult<DayDataObject>> GetDayDataObject(CancellationToken ctx)
        {
            return GetMockData<DayDataObject>("Traveler.DAL.Resources.Mock.Main.day1.json");
        }

        public Task<RequestResult<List<EventDataObject>>> GetEventAsync(int id, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult<EventDataObject>> GetEventDataObject(CancellationToken ctx)
        {
            return GetMockData<EventDataObject>("Traveler.DAL.Resources.Mock.Main.event1.json");       
        }

        public Task<RequestResult<List<EventDataObject>>> GetEventsOfTheDayAsync(int idTrav, DateTime date, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult<TravelDataObject>> GetTravelDataObject(CancellationToken ctx)
        {
            return GetMockData<TravelDataObject>("Traveler.DAL.Resources.Mock.Main.travel1.json");
        }

        public Task<RequestResult<List<TravelDataObject>>> GetTravelsAsync(CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult> SaveEventAsync(EventDataObject item, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult> SaveTravelAsync(TravelDataObject item, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }
    }
}
