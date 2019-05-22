using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Traveler.DAL.DataObjects;
using Traveler.DAL.DataServices;

namespace Traveler.BL.ViewModels.Main
{
    class DisplayViewModel : BaseViewModel
    {
        public List<EventDataObject> Events
        {
            get => Get<List<EventDataObject>>();
            private set => Set(value);
        }

        public bool IsEmpty => Events.Count == 0;
        public bool IsNotEmpty => Events.Count != 0;

        public override async Task OnPageAppearing()
        {
            State = PageState.Loading;
            var result = await DataServices.TravelerDataService.GetEventsOfCurrentDayAsync(DateTime.Today, CancellationToken);
            if (result.IsValid)
            {
                Events = result.Data;
                State = PageState.Normal;
            }
            else
            {
                State = PageState.Error;
            }
        }
    }
}
