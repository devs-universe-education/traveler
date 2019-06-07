using System;

namespace Traveler.DAL.DataServices
{
    public static class DataServices
    {
        public static IMainDataService Main { get; private set; }

        public static ITravelerDataService TravelerDataService { get; private set; }

        public static void Init(bool isMock, string connectionString)
        {
            if (isMock)
            {
                TravelerDataService = new Mock.MockTravelerDataService();
            }
            else
            {
                TravelerDataService = new Database.DatabaseTraveler(connectionString);               
            }
        }
    }
}

