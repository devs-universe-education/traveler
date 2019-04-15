using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Traveler.DAL.DataObjects;

namespace Traveler.DAL.DataServices.Mock
{
    class MockTravelDataService : BaseMockDataService, ITravelDataService
    {
        public Task<RequestResult<DayDataObject>> GetDayDataObject(CancellationToken ctx)
        {
            return GetMockData<DayDataObject>("Traveler.DAL.Resources.Mock.Main.day1.json");
        }

        public Task<RequestResult<EventDataObject>> GetEventDataObject(CancellationToken ctx)
        {
            return GetMockData<EventDataObject>("Traveler.DAL.Resources.Mock.Main.event1.json");       
        }

        public Task<RequestResult<TravelDataObject>> GetTravelDataObject(CancellationToken ctx)
        {
            return GetMockData<TravelDataObject>("Traveler.DAL.Resources.Mock.Main.travel1.json");
        }
    }
}
