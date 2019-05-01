using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Traveler.DAL.DataObjects;

namespace Traveler.DAL.DataServices
{
    public interface ITravelDataService
    {
        Task<RequestResult<DayDataObject>> GetDayDataObject(CancellationToken ctx);
        Task<RequestResult<EventDataObject>> GetEventDataObject(CancellationToken ctx);
        Task<RequestResult<Travel>> GetTravelDataObject(CancellationToken ctx);
    }
}
