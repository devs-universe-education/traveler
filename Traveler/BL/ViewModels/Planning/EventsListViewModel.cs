using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Traveler.DAL.DataObjects;
using Traveler.DAL.DataServices;

namespace Traveler.BL.ViewModels.Planning
{
    class EventsListViewModel : BaseViewModel
    {
        public ICommand GoToEventDescriptionCommand => MakeNavigateToCommand(AppPages.EventDescription);
        public ICommand GoToEventNameCommand => MakeNavigateToCommand(AppPages.EventName);

        public DayDataObject ListEventObject
        {
            get => Get<DayDataObject>();
            private set => Set(value);
        }


        public override async Task OnPageAppearing()
        {
            State = PageState.Loading;
            var result = await DataServices.MainTravel.GetDayDataObject(CancellationToken);
            if (result.IsValid)
            {
                ListEventObject = result.Data;
                State = PageState.Normal;
            }
            else
                State = PageState.Error;
        }

    }
}
