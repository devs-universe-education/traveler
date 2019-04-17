using System;

namespace Traveler.DAL.DataServices
{
    public static class DataServices
    {
        public static IMainDataService Main { get; private set; }

        public static ITravelDataService TravelMock { get; private set; }

        public static void Init(bool isMock)
        {
            if (isMock)
            {
                TravelMock = new Mock.MockTravelDataService();
            }
            else
            {
                throw new NotImplementedException("Online Data Services not implemented");
            }
        }

    }
}

