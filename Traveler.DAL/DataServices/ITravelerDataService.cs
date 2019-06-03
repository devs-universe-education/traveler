using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Traveler.DAL.DataObjects;

namespace Traveler.DAL.DataServices
{
    public interface ITravelerDataService
    {
        Task<RequestResult<List<TravelDataObject>>> GetTravelsAsync(CancellationToken ctx);
        Task<RequestResult<List<TravelDataObject>>> GetTravelsOfMonthAsync(DateTime date, CancellationToken ctx);
        Task<RequestResult> SaveTravelAsync(TravelDataObject item, CancellationToken ctx);
        Task<RequestResult> DeleteTravelAsync(TravelDataObject item, CancellationToken ctx);

        Task<RequestResult<DayDataObject>> GetDayAsync(int idTravel, DateTime day, CancellationToken ctx);

        Task<RequestResult<List<EventDataObject>>> GetEventsOfDayAsync(int idTravel, DateTime day, CancellationToken ctx);
        Task<RequestResult<List<EventDataObject>>> GetEventsOfCurrentDayAsync(DateTime today, CancellationToken ctx);        
        Task<RequestResult> SaveEventAsync(EventDataObject item, CancellationToken ctx);
        Task<RequestResult> DeleteEventAsync(EventDataObject item, CancellationToken ctx);
    }
}
