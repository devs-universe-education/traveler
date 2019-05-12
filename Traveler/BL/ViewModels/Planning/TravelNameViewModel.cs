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
            get => Get<DateTime>();
            set => Set(value);
        }

        public DateTime NewTravelEndDate
        {
            get => Get<DateTime>();
            set => Set(value);
        }

        public override async Task OnPageAppearing()
        {
            var year = (int)(NavigationParams["Year"]);
            var month = (int)(NavigationParams["Month"]);
            var (startDay, endDay) = (ValueTuple<int, int>)(NavigationParams["Days"]);

            NewTravelStartDate = new DateTime(year, month, startDay);
            NewTravelEndDate = new DateTime(year, month, endDay);
        }        
    }
}
