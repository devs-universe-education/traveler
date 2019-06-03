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
                    execute: async () =>
                    {
                        TravelDataObject travel = new TravelDataObject()
                        {
                            Title = TravelTitle,
                            StartDate = NewTravelStartDate,
                            EndDate = NewTravelEndDate
                        };

                        await DataServices.TravelerDataService.SaveTravelAsync(travel, CancellationToken);
                        NavigateBack();
                    });
            }
        }

        public override async Task OnPageAppearing()
        {
            var (startDate, endDate) = (ValueTuple<DateTime, DateTime>)(NavigationParams["Dates"]);

            NewTravelStartDate = startDate;
            NewTravelEndDate = endDate == default(DateTime) ? startDate : endDate;
        }        
    }
}
