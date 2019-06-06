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
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;

namespace Traveler.Android
{
    [BroadcastReceiver]
    class AlarmNotificationReceiver : BroadcastReceiver
    {
        static readonly int NOTIFICATION_ID = 101010;
        public override void OnReceive(Context context, Intent intent)
        {
            var resultIntent = new Intent(context, typeof(MainActivity));

            var stackBuilder = TaskStackBuilder.Create(context);
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainActivity)));
            stackBuilder.AddNextIntent(resultIntent);

            var resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

            Notification notification;

            var CHANNEL_ID = context.GetString(Resource.String.channel_id);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(context, CHANNEL_ID);
            builder.SetAutoCancel(true)
                   .SetContentIntent(resultPendingIntent)
                   .SetContentTitle("Напоминание")
                   .SetContentText("До ближайшего события осталось 30 минут!")
                   .SetSmallIcon(Resource.Drawable.icon);

            notification = builder.Build();

            var notificationManager = NotificationManagerCompat.From(context);
            notificationManager.Notify(NOTIFICATION_ID, notification);
        }
    }
}