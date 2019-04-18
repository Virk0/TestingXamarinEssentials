using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Xamarin.Essentials;
using System;

namespace TestingXamarinEssentials
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            #region Battery
            var level = Battery.ChargeLevel; // returns 0.0 to 1.0 or 1.0 when on AC or no battery.

            var state = Battery.State;

            switch (state)
            {
                case BatteryState.Charging:
                    // Currently charging
                    break;
                case BatteryState.Full:
                    // Battery is full
                    break;
                case BatteryState.Discharging:
                case BatteryState.NotCharging:
                    // Currently discharging battery or not being charged
                    break;
                case BatteryState.NotPresent:
                // Battery doesn't exist in device (desktop computer)
                case BatteryState.Unknown:
                    // Unable to detect battery state
                    break;
            }

            var source = Battery.PowerSource;

            switch (source)
            {
                case BatteryPowerSource.Battery:
                    // Being powered by the battery
                    break;
                case BatteryPowerSource.AC:
                    // Being powered by A/C unit
                    break;
                case BatteryPowerSource.Usb:
                    // Being powered by USB cable
                    break;
                case BatteryPowerSource.Wireless:
                    // Powered via wireless charging
                    break;
                case BatteryPowerSource.Unknown:
                    // Unable to detect power source
                    break;
            }
            TextView batteryLevel = (TextView)FindViewById(Resource.Id.batterystatus);
            TextView batteryState = (TextView)FindViewById(Resource.Id.batterystate);
            TextView batterySource = (TextView)FindViewById(Resource.Id.batterysource);
            var levelAdjusted = level * 100;
            batteryLevel.Text = ("Battery Level: " + levelAdjusted + "%");
            batteryState.Text = ("Battery State: " + state);
            batterySource.Text = ("Battery Source: " + source);
            #endregion
            #region vibration
            var VibrateBtn = (Button)FindViewById(Resource.Id.VibrateButton);
            VibrateBtn.Click += VibrateBtn_Click;

            #endregion
            #region deviceinfo
            // Device Model (SMG-950U, iPhone10,6)
            var device = DeviceInfo.Model;

            // Manufacturer (Samsung)
            var manufacturer = DeviceInfo.Manufacturer;

            // Device Name (Motz's iPhone)
            var deviceName = DeviceInfo.Name;

            // Operating System Version Number (7.0)
            var version = DeviceInfo.VersionString;

            // Platform (Android)
            var platform = DeviceInfo.Platform;

            // Idiom (Phone)
            var idiom = DeviceInfo.Idiom;

            // Device Type (Physical)
            var deviceType = DeviceInfo.DeviceType;
            TextView deviceModel = FindViewById<TextView>(Resource.Id.devicemodel);
            deviceModel.Text = ("Device Model: " + device);
            TextView androidVersion = FindViewById<TextView>(Resource.Id.androidversion);
            androidVersion.Text = ("Android Version: " + version);
            #endregion
            #region flashlight  
            Button FlashLightON = FindViewById<Button>(Resource.Id.FlashON);
            Button FlashLightOFF = FindViewById<Button>(Resource.Id.FlashOFF);
            FlashLightON.Click += FlashLightON_ClickAsync;
            FlashLightOFF.Click += FlashLightOFF_ClickAsync;
            #endregion
        }

        private async void FlashLightOFF_ClickAsync(object sender, EventArgs e)
        {
            await Flashlight.TurnOffAsync();
        }

        private async void FlashLightON_ClickAsync(object sender, EventArgs e)
        {
            await Flashlight.TurnOnAsync();
        }

        private void VibrateBtn_Click(object sender, EventArgs e)
        {
            Vibration.Vibrate();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}