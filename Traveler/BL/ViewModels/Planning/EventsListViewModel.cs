using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Traveler.BL.ViewModels.Planning
{
    class EventsListViewModel : BaseViewModel
    {
        public ICommand GoToEventDescriptionCommand => MakeNavigateToCommand(AppPages.EventDescription);
        public ICommand GoToEventNameCommand => MakeNavigateToCommand(AppPages.EventName);
    }
}
