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
            throw new NotImplementedException();
        }

        public Task<RequestResult<List<TravelDataObject>>> GetTravelsOfMonthAsync(DateTime today, CancellationToken ctx)
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

        public Task<RequestResult<DayDataObject>> GetDayAsync(int idTravel, DateTime day, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult<List<EventDataObject>>> GetEventsOfDayAsync(int idTravel, DateTime day, CancellationToken ctx)
        {
            throw new NotImplementedException();
        }

        public Task<RequestResult<List<EventDataObject>>> GetEventsOfCurrentDayAsync(DateTime today, CancellationToken ctx)
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
    }
}
