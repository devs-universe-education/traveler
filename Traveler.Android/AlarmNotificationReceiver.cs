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
        public override void OnReceive(Context context, Intent intent)
        {

            NotificationChannel notificationChannel = new NotificationChannel("myChannel", "name", NotificationImportance.High);

            NotificationManager notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            notificationManager.CreateNotificationChannel(notificationChannel);


            NotificationCompat.Builder builder = new NotificationCompat.Builder(context, "mychannel");
            builder.SetContentTitle("NOTIFICATION")
                   .SetContentText("aboit notification")
                   .SetSmallIcon(Resource.Drawable.notification_template_icon_low_bg)
                   .SetChannelId("myChannel");

            Notification notification = builder.Build();

            notificationManager.Notify(12, notification);
        }
    }
}