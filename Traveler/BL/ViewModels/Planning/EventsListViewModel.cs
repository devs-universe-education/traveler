using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Traveler.DAL.DataObjects;
using Traveler.DAL.DataServices;
using Xamarin.Forms;

namespace Traveler.BL.ViewModels.Planning
{
    class EventsListViewModel : BaseViewModel
    {
        private DayDataObject eventParent;

        public List<EventDataObject> Events
        {
            get => Get<List<EventDataObject>>();
            private set
            {
                Set(value);
                IsEmpty = value.Count == 0;
                IsNotEmpty = value.Count != 0;
            }
        }

        public bool IsEmpty
        {
            get => Get<bool>();
            private set => Set(value);
        }

        public bool IsNotEmpty
        {
            get => Get<bool>();
            private set => Set(value);
        }

        public string PageTitle
        {
            get => Get<string>();
            private set => Set(value);
        }

        public ICommand GoToEventDescriptionCommand
        {
            get
            {
                return new Command(
                    execute: (parameter) =>
                    {
                        NavigateTo(AppPages.EventDescription, null, dataToLoad: new Dictionary<string, object>()
                        {
                            { "parameter", parameter },
                            { "date", eventParent.Date }
                        });
                    });
            }
        }

        public ICommand GoToNewEventDescriptionCommand
        {
            get
            {
                return new Command(
                    execute: () =>
                    {
                        EventDataObject newEvent = new EventDataObject() { IdDay = eventParent.Id };

                        NavigateTo(AppPages.EventDescription, null, dataToLoad: new Dictionary<string, object>()
                        {
                            { "parameter", newEvent },
                            { "date", eventParent.Date }
                        });
                    });
            }
        }        

        public override async Task OnPageAppearing()
        {
            State = PageState.Loading;

            object obj = NavigationParams["parameter"];
            var (id, day) = (ValueTuple<int, DateTime>)obj;

            var eventResult = await DataServices.TravelerDataService.GetEventsOfDayAsync(id, day, CancellationToken);
            var dayResult = await DataServices.TravelerDataService.GetDayAsync(id, day, CancellationToken);

            if (eventResult.IsValid && dayResult.IsValid)
            {
                Events = eventResult.Data;
                eventParent = dayResult.Data;

                PageTitle = "Список событий " + eventParent.Date.ToString("dd/MM/yyyy");

                State = PageState.Normal;
            }
            else
            {
                State = PageState.Error;
            }
        }
    }    
}
