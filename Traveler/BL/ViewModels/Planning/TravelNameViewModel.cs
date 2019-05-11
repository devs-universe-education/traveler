using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.BL.ViewModels.Planning
{
    class TravelNameViewModel : BaseViewModel
    {
        public DateTime NewTravelStartDate
        {
            get => newTravelStartDate;
            private set
            {
                newTravelStartDate = value;
                OnPropertyChanged(nameof(NewTravelStartDate));
            }
        }
        public DateTime newTravelStartDate;

        public DateTime NewTravelEndDate
        {
            get => newTravelEndDate;
            private set
            {
                newTravelEndDate = value;
                OnPropertyChanged(nameof(NewTravelEndDate));
            }
        }
        private DateTime newTravelEndDate;

        public override async Task OnPageAppearing()
        {
            var year = (int)(NavigationParams["Year"]);
            var month = (int)(NavigationParams["Month"]);
            var days = (Tuple<int, int>)(NavigationParams["Days"]);

            NewTravelStartDate = new DateTime(year, month, days.Item1);
            NewTravelEndDate = new DateTime(year, month, days.Item2);
        }        
    }
}
