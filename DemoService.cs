using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace MobileClientXamarin
{
    [Service(Exported = true, Name = "com.xamarin.example.DemoService")]
    public class DemoService : Service
    {
        public static AppCompatActivity activity;
        AudioService audioService;
        Timer timer;
        // Magical code that makes the service do wonderful things.
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            timer.Dispose();
            timer = null;
            Toast.MakeText(this, $"OnDestroy", ToastLength.Long).Show();
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Toast.MakeText(this, $"Start timer ", ToastLength.Long).Show();

            audioService = new AudioService();

            timer = new Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 30_000;
            timer.Enabled = true;
            timer.Start();


            var text = BatteryAlarm.Check();
            Toast.MakeText(this, text, ToastLength.Long).Show();

            return StartCommandResult.Sticky;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            int level = BatteryAlarm.GetLevelInPercent();
            if (level >= 90)
            {
                activity.RunOnUiThread(() =>
                {
                    BatteryAlarm.ifoView.Text = $"ChargeLevel = {level}!!!\nStop charge!!!";
                    audioService.PlayFile();
                    //    //Toast.MakeText(ApplicationContext, text, ToastLength.Long).Show();
                });
            }

        }
    }
}