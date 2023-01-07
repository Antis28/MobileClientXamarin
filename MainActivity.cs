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
            InitLocalText();
        }

        private void InitLocalText()
        {
            var textView = FindViewById<TextView>(Resource.Id.textView1);
            textView.Text = GetString(Resource.String.hello_world_text);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {           
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private void CreateButtonHandler()
        {
            var bntOk = FindViewById<Button>(Resource.Id.button1);
            bntOk.Click += BtnOk_Click;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {

            var alert = new AndroidX.AppCompat.App.AlertDialog.Builder(this);
            alert.SetTitle(GetString(Resource.String.attention));
            alert.SetMessage(GetString(Resource.String.attention_message));
            alert.SetCancelable(false);
            alert.SetPositiveButton(GetString(Resource.String.yes), delegate { Java.Lang.JavaSystem.Exit(0); });
            alert.SetNegativeButton(GetString(Resource.String.no), delegate { });

            RunOnUiThread(() =>
            {
                alert.Show();
            }
            );
        }
    }
}