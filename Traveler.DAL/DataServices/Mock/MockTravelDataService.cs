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

        public Task<RequestResult<Travel>> GetTravelDataObject(CancellationToken ctx)
        {
            return GetMockData<Travel>("Traveler.DAL.Resources.Mock.Main.travel.json");
        }
    }
}
