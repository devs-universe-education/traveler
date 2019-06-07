using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

using Traveler.DAL.DataServices;
using Plugin.Settings;

using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;

namespace Traveler.Android
{
    [BroadcastReceiver]
    class AlarmNotificationReceiver : BroadcastReceiver
    {
        private static readonly int NOTIFICATION_ID = 101010;

        public override async void OnReceive(Context context, Intent intent)
        {
            bool pushesEnabled = CrossSettings.Current.GetValueOrDefault("PushesEnabled", true);
            if (!pushesEnabled)
                return;

            DateTime date = DateTime.Now;
            DateTime eventTime = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0).AddMinutes(30);

            if (DataServices.TravelerDataService == null)
                DataServices.Init(false, new DatabaseConnectionAndroid().GetConnectionString());

            var result = await DataServices.TravelerDataService.GetEventTitleAsync(eventTime);
            if(result.IsValid)
            {
                var CHANNEL_ID = context.GetString(Resource.String.channel_id);

                var resultIntent = new Intent(context, typeof(MainActivity));

                var stackBuilder = TaskStackBuilder.Create(context);
                stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainActivity)));
                stackBuilder.AddNextIntent(resultIntent);

                var resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

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