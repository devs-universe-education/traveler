using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Traveler.BL.ViewModels.Planning
{
    class CalendarViewModel : BaseViewModel
    {
        public ICommand GoToTravelNameCommand => GetNavigateToCommand(AppPages.TravelName);
        public ICommand GoToEventsListCommand => GetNavigateToCommand(AppPages.EventsList);
    }
}
