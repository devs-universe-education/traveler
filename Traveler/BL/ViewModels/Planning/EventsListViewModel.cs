using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Traveler.BL.ViewModels.Planning
{
    class EventsListViewModel : BaseViewModel
    {
        public ICommand GoToEventDescriptionCommand => GetNavigateToCommand(AppPages.EventDescription);
        public ICommand GoToEventNameCommand => GetNavigateToCommand(AppPages.EventName);
    }
}
