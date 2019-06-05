using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Traveler.DAL.DataObjects;
using Traveler.DAL.DataServices;
using Xamarin.Forms;

namespace Traveler.BL.ViewModels.Planning
{
    class EventDescriptionViewModel : BaseViewModel
    {
        public EventDataObject Event
        {
            get => Get<EventDataObject>();
            private set => Set(value);
        }

        public TimeSpan StartTime
        {
            get => Get<TimeSpan>();
            set => Set(value);
        }

        public TimeSpan EndTime
        {
            get => Get<TimeSpan>();
            set => Set(value);
        }

        public ICommand DeleteEventCommand
        {
            get
            {
                return new Command(
                    execute: async () =>
                    {
                        bool questionResult = await ShowQuestion("Подтверждение действия", "Удалить событие?", "Да", "Нет");
                        if(questionResult)
                        {
                            var queryResult = await DataServices.TravelerDataService.DeleteEventAsync(Event, CancellationToken);
                            if(queryResult.Status == DAL.RequestStatus.Ok)
                            {
                                ShowAlert("", "Событие удалено", "OK");
                                NavigateBack();
                            }
                            else
                            {
                                ShowAlert("", "Ошибка при удалении", "OK");
                            }
                        }
                    });
            }
        }

        public ICommand SaveEventCommand
        {
            get
            {
                return new Command(
                    execute: async () =>
                    {
                        Event.StartTime = new DateTime(1, 1, 1, StartTime.Hours, StartTime.Minutes, 0);
                        Event.EndTime = new DateTime(1, 1, 1, EndTime.Hours, EndTime.Minutes, 0);
                        await DataServices.TravelerDataService.SaveEventAsync(Event, CancellationToken);
                        NavigateBack();
                    });
            }
        }

        public override async Task OnPageAppearing()
        {
            if (NavigationParams == null)
            {
                State = PageState.Error;
                return;
            }

            if (NavigationParams.TryGetValue("parameter", out object value) && value is EventDataObject evnt)
            {
                Event = evnt;
                StartTime = new TimeSpan(evnt.StartTime.Hour, evnt.StartTime.Minute, 0);
                EndTime = new TimeSpan(evnt.EndTime.Hour, evnt.EndTime.Minute, 0);
                State = PageState.Normal;
            }
            else
            {
                State = PageState.Error;
            }
        }
    }
}
