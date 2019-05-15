using System;
using Traveler.DAL.dbSQLite;

namespace Traveler.DAL.DataServices
{
    public static class DataServices
    {
        public static IMainDataService Main { get; private set; }

        public static ITravelDataService TravelMock { get; private set; }

        public static DatabaseTraveler TravelDB { get; private set; }

        public static void Init(bool isMock)
        {
            if (isMock)
            {
                TravelMock = new Mock.MockTravelDataService();
            }
            else
            {
                TravelDB = new DatabaseTraveler();
                //throw new NotImplementedException("Online Data Services not implemented");
            }
        }
    }
}

