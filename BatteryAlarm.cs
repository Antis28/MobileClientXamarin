using Android.Widget;
using AndroidX.AppCompat.Widget;
using System;
using System.Timers;
using Xamarin.Essentials;


namespace MobileClientXamarin
{
    internal class BatteryAlarm
    {
        public static TextView ifoView;
        
        public static int GetLevelInPercent()
        {
            double level = Battery.ChargeLevel; // returns 0.0 to 1.0 or 1.0 when on AC or no battery.            
            return (int)(level * 100);
        }


        public static void Register()
        {
            audioService = new AudioService();
            // Register for battery changes, be sure to unsubscribe when needed
            Battery.BatteryInfoChanged += Battery_BatteryInfoChanged;
        }

        public static string Check()
        {
            var level = Battery.ChargeLevel; // returns 0.0 to 1.0 or 1.0 when on AC or no battery.

            var state = Battery.State;

            var text = String.Empty;

            text += $"ChargeLevel = {level * 100}\n";
            switch (state)
            {
                case BatteryState.Charging:
                    text += $"Currently charging";
                    // Currently charging
                    break;
                case BatteryState.Full:
                    // Battery is full
                    text += $"Battery is full";
                    break;
                case BatteryState.Discharging:
                case BatteryState.NotCharging:
                    // Currently discharging battery or not being charged
                    text += $"Currently discharging battery or not being charged";
                    break;
                case BatteryState.NotPresent:
                    // Battery doesn't exist in device (desktop computer)
                    text += $"Battery doesn't exist in device (desktop computer)";
                    break;
                case BatteryState.Unknown:
                    // Unable to detect battery state
                    text += $"Unable to detect battery state";
                    break;
            }
            text += "\n";

            var source = Battery.PowerSource;

            switch (source)
            {
                case BatteryPowerSource.Battery:
                    // Being powered by the battery
                    text += "Being powered by the battery \n";
                    break;
                case BatteryPowerSource.AC:
                    // Being powered by A/C unit
                    text += "Being powered by A/C unit \n";
                    break;
                case BatteryPowerSource.Usb:
                    // Being powered by USB cable
                    text += "Being powered by USB cable \n";
                    break;
                case BatteryPowerSource.Wireless:
                    // Powered via wireless charging
                    text += "Powered via wireless charging\n";
                    break;
                case BatteryPowerSource.Unknown:
                    // Unable to detect power source
                    text += "Unable to detect power source\n";
                    break;
            }
            return text;
        }


        public static void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
        {
            var level = e.ChargeLevel;
            var state = e.State;
            var source = e.PowerSource;
            Console.WriteLine($"Reading: Level: {level}, State: {state}, Source: {source}");
            ifoView.Text = $"Reading: Level: {level * 100}, State: {state}, Source: {source}";
            
            audioService.PlayFile();
        }
        static AudioService audioService;


    }
}