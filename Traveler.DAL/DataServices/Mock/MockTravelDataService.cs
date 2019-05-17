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
        public Task<RequestResult<List<TravelDataObject>>> GetTravelsAsync(CancellationToken ctx)
        {
            return GetMockDataList<TravelDataObject>("Traveler.DAL.Resources.Mock.Main.travel1.json");
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
            return GetMockDataList<EventDataObject>("Traveler.DAL.Resources.Mock.Main.event1.json");
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
    }
}
