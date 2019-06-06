using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Traveler.DAL.DataServices;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;

namespace Traveler.Android
{
    [BroadcastReceiver]
    class AlarmNotificationReceiver : BroadcastReceiver
    {
        static readonly int NOTIFICATION_ID = 101010;
        public override async void OnReceive(Context context, Intent intent)
        {
            var resultIntent = new Intent(context, typeof(MainActivity));

            var stackBuilder = TaskStackBuilder.Create(context);
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainActivity)));
            stackBuilder.AddNextIntent(resultIntent);

            var resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);           

            DateTime date = DateTime.Now;
            DateTime dateTimeStart = new DateTime(1, 1, 1, date.Hour, date.Minute, 0).AddMinutes(30);

            var result = await DataServices.TravelerDataService.GetEventTitleAsync(dateTimeStart);

            if(result.IsValid)
            {
                var CHANNEL_ID = context.GetString(Resource.String.channel_id);

                NotificationCompat.Builder builder = new NotificationCompat.Builder(context, CHANNEL_ID);
                builder.SetAutoCancel(true)
                       .SetContentIntent(resultPendingIntent)
                       .SetContentTitle($"Событие '{result.Data}'")
                       .SetContentText("Осталось 30 минут!")
                       .SetSmallIcon(Resource.Drawable.icon);

                Notification notification = builder.Build();

                var notificationManager = NotificationManagerCompat.From(context);
                notificationManager.Notify(NOTIFICATION_ID, notification);
            }
           
        }
    }
}