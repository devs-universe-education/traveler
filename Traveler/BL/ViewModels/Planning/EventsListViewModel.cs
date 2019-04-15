using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public DayDataObject DayObject
        {
            get => Get<DayDataObject>();
            private set => Set(value);
        }

        ObservableCollection<EventDataObject> dayCollection;

        public ObservableCollection<EventDataObject> DayCollection
        {
            get => dayCollection ?? new ObservableCollection<EventDataObject>();
            set => dayCollection = value;
        }


        public override async Task OnPageAppearing()
        {
            State = PageState.Loading;
            var result = await DataServices.Main.GetDayDataObject(CancellationToken);
            if (result.IsValid)
            {
                DayCollection = new ObservableCollection<EventDataObject>(result.Data);
               // DayObject = result.Data;
                State = PageState.Normal;
            }
            else
                State = PageState.Error;
        }
    }
}
