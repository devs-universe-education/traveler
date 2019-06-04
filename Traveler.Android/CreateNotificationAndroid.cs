using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Traveler.Android;


[assembly: Xamarin.Forms.Dependency(typeof(CreateNotificationAndroid))]
namespace Traveler.Android
{
    class CreateNotificationAndroid : INotificationCreate
    {
        AlarmManager alarmManager;
        Intent myIntent;
        PendingIntent pendingIntent;
        
        public CreateNotificationAndroid()
        {
            alarmManager = (AlarmManager)Application.Context.GetSystemService(Context.AlarmService);            
        }

        public void CreateNotification()
        {
            myIntent = new Intent(Application.Context, typeof(AlarmNotificationReceiver));
            pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, myIntent, PendingIntentFlags.UpdateCurrent);
            alarmManager.Set(AlarmType.RtcWakeup, SystemClock.ElapsedRealtime() + 3000, pendingIntent);
        }
    }
}