using System.Threading;
using System.Threading.Tasks;
using Traveler.DAL.DataObjects;

namespace Traveler.DAL.DataServices.Mock
{
    class MockMainDataService : BaseMockDataService, IMainDataService
    {
        public Task<RequestResult<SampleDataObject>> GetSampleDataObject(CancellationToken ctx)
        {
            return GetMockData<SampleDataObject>("Traveler.DAL.Resources.Mock.Main.SampleDataObject.json");
        }

        public Task<RequestResult<DayDataObject>> GetDayDataObject(CancellationToken ctx)
        {
            return GetMockData<DayDataObject>("Traveler.DAL.Resources.Mock.Main.day1.json");
        }
    }
}

