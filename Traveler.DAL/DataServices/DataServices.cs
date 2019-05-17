using System;
using Traveler.DAL.DataServices.Database;

namespace Traveler.DAL.DataServices
{
    public static class DataServices
    {
        public static IMainDataService Main { get; private set; }

        public static ITravelerDataService TravelerDataService { get; private set; }

       
        public static void Init(bool isMock)
        {
            if (isMock)
            {
                TravelerDataService = new Mock.MockTravelDataService();
            }
            else
            {
                TravelerDataService = new Database.DatabaseTraveler();               
            }
        }
    }
}

