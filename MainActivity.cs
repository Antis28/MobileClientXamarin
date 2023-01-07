using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using System;

namespace MobileClientXamarin
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            CreateButtonHandler();
        }        

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {           
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private void CreateButtonHandler()
        {
            var bntOk = FindViewById<Button>(Resource.Id.button1);
            bntOk.Click += BntOk_Click;
        }

        private void BntOk_Click(object sender, EventArgs e)
        {

            var alert = new AndroidX.AppCompat.App.AlertDialog.Builder(this);
            alert.SetTitle("Внимание");
            alert.SetMessage("Вы действительно хотите выйти из приложения?");
            alert.SetCancelable(false);
            alert.SetPositiveButton("Да", delegate { Java.Lang.JavaSystem.Exit(0); });
            alert.SetNegativeButton("Нет", delegate { });

            RunOnUiThread(() =>
            {
                alert.Show();
            }
            );
        }
    }
}