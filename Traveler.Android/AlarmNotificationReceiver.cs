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
        static readonly string CHANNEL_ID = "location_notification";
        static readonly string CHANNEL_NAME = "name";

        public override void OnReceive(Context context, Intent intent)
        {
            NotificationManager notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            Notification notification;

            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {                             
                NotificationCompat.Builder builder = new NotificationCompat.Builder(context);
                builder.SetContentTitle("Traveler")
                       .SetContentText("До ближайшего события осталось 30 минут!")
                       .SetSmallIcon(Resource.Drawable.notification_template_icon_low_bg);
                notification = builder.Build();
            }
            else
            {
                NotificationChannel notificationChannel = new NotificationChannel(CHANNEL_ID, CHANNEL_NAME, NotificationImportance.High);
              
                notificationManager.CreateNotificationChannel(notificationChannel);

                NotificationCompat.Builder builder = new NotificationCompat.Builder(context, CHANNEL_ID);
                builder.SetContentTitle("Traveler")
                       .SetContentText("До ближайшего события осталось 30 минут!")
                       .SetSmallIcon(Resource.Drawable.notification_template_icon_low_bg)
                       .SetChannelId(CHANNEL_ID);
                notification = builder.Build();

            }            
            notificationManager.Notify(NOTIFICATION_ID, notification);
        }
    }
}