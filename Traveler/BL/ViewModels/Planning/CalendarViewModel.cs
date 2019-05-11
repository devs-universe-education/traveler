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
    class CalendarViewModel : BaseViewModel
    {
        //public ICommand GoToTravelNameCommand => MakeNavigateToCommand(AppPages.TravelName);
        //public ICommand GoToEventsListCommand => MakeNavigateToCommand(AppPages.EventsList);

        #region TravelerCalendarTest

        public int Year => DateTime.Now.Year;
        public int Month => DateTime.Now.Month;

        public ICommand GoToTravelNameCommand
        {
            get
            {
                return new Command((parameter) =>
                {
                    NavigateTo(AppPages.TravelName, null, dataToLoad: new Dictionary<string, object>()
                    {
                        { "Year", Year },
                        { "Month", Month },
                        { "Days", parameter }
                    });
                });
            }
        }

        public ICommand GoToEventsListCommand
        {
            get => new Command((parameter) => NavigateTo(AppPages.EventsList, null, dataToLoad: new Dictionary<string, object>() { { "param", parameter } }));
        }

        public List<Travel> Travels
        {
            get => Get<List<Travel>>();
            private set => Set(value);
        }

        public override async Task OnPageAppearing()
        {
            State = PageState.Loading;
            var result = await DataServices.TravelMock.GetTravelDataObject(CancellationToken);
            if (result.IsValid)
            {
                Travels = new List<Travel>() { result.Data };
                State = PageState.Normal;
            }
            else
                State = PageState.Error;
        }

        #endregion
    }
}
