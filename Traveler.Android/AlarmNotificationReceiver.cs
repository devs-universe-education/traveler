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

namespace Traveler.Android
{
    [BroadcastReceiver]
    class AlarmNotificationReceiver : BroadcastReceiver
    {
        static readonly int NOTIFICATION_ID = 101010;
        static readonly string CHANNEL_ID = "channelId";
        public override void OnReceive(Context context, Intent intent)
        {
            Notification notification;

            NotificationCompat.Builder builder = new NotificationCompat.Builder(context, CHANNEL_ID);
            builder.SetAutoCancel(true)
                   .SetContentTitle("Напоминание")
                   .SetContentText("Осталось 30 минут!")
                   .SetSmallIcon(Resource.Drawable.icon);
                      
                notification = builder.Build();

            var notificationManager = NotificationManagerCompat.From(context);
            notificationManager.Notify(NOTIFICATION_ID, notification);
        }
    }
}