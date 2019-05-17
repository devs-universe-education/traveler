using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Traveler.DAL.DataObjects;
using Traveler.DAL.DataServices;

namespace Traveler.BL.ViewModels.Planning
{
    class EventDescriptionViewModel : BaseViewModel
    {
        public EventDataObject EventObject
        {
            get => Get<EventDataObject>();
            private set => Set(value);
        }

        public override async Task OnPageAppearing()
        {
            State = PageState.Loading;
            var result = await DataServices.TravelerDataService.GetEventDataObject(CancellationToken);
            if (result.IsValid)
            {
                EventObject = result.Data;
                State = PageState.Normal;
            }
            else
                State = PageState.Error;
        }

    }
}
