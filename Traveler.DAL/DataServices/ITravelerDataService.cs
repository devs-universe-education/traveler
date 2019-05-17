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
        Task<RequestResult<DayDataObject>> GetDayDataObject(CancellationToken ctx);
        Task<RequestResult<EventDataObject>> GetEventDataObject(CancellationToken ctx);
        Task<RequestResult<TravelDataObject>> GetTravelDataObject(CancellationToken ctx);


        Task<RequestResult<List<TravelDataObject>>> GetTravelsAsync(CancellationToken ctx);
        Task<RequestResult> SaveTravelAsync(TravelDataObject item, CancellationToken ctx);
        Task<RequestResult> DeleteTravelAsync(TravelDataObject item, CancellationToken ctx);

        Task<RequestResult> DeleteItemsDaysByTravelAsync(int idTravel, CancellationToken ctx);

        Task<RequestResult<List<EventDataObject>>> GetEventAsync(int id, CancellationToken ctx);
        Task<RequestResult<List<EventDataObject>>> GetEventsOfTheDayAsync(int idTrav, DateTime date, CancellationToken ctx);        
        Task<RequestResult> SaveEventAsync(EventDataObject item, CancellationToken ctx);
        Task<RequestResult> DeleteEventAsync(EventDataObject item, CancellationToken ctx);
        Task<RequestResult> DeleteEventsByDayAsync(int idDay, CancellationToken ctx);

       
    }
}
