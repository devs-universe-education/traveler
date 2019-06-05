using System;
using System.Collections.Generic;
using System.Text;

namespace Traveler
{
    public interface INotificationCreate
    {
        void CreateNotification(int eventStartTimeHours, int eventStartTimeMinutes);
    }
}
