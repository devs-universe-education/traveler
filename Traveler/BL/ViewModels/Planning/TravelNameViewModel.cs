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

        public string TravelTitle
        {
            get => Get<string>();
            set => Set(value);
        }

        public ICommand CreateTravelCommand
        {
            get
            {
                return new Command(
                    execute: () =>
                    {
                        TravelDataObject travel = new TravelDataObject()
                        {
                            Title = TravelTitle,
                            StartDate = NewTravelStartDate,
                            EndDate = NewTravelEndDate.Year == 1900 ? NewTravelStartDate : NewTravelEndDate
                        };

                        DataServices.TravelerDataService.SaveTravelAsync(travel, CancellationToken);
                        NavigateBack();
                    });
            }
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
