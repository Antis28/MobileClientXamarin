using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using System;
using AndroidX.AppCompat.View.Menu;
using Android.Content;

namespace MobileClientXamarin
{

    [Activity(Label = "@string/app_name",
            Icon = "@mipmap/ic_launcher",
            Theme = "@style/AppTheme",
            MainLauncher = true
        )]
    public class MainActivity : AppCompatActivity
    {
        Client clientView;
        EditText edit;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //CreateButtonHandler();
            InitLocalText();

            var toolbar = FindViewById<ListMenuItemView>(Resource.Id.toolbar);
            edit = toolbar.FindViewById<EditText>(Resource.Id.editRepeatCount);

        }
                
        private void InitLocalText()
        {
            //var textView = FindViewById<TextView>(Resource.Id.textView1);
            //textView.Text = GetString(Resource.String.hello_world_text);

            var serverIpAddressEdit = FindViewById<EditText>(Resource.Id.serverIpAddressEdit);
            var playerNameText = FindViewById<TextView>(Resource.Id.playerNameText);

            clientView = new Client(serverIpAddressEdit, playerNameText);

            var btnLeft = FindViewById<Button>(Resource.Id.buttonLeft);
            var btnRight = FindViewById<Button>(Resource.Id.buttonRight);
            var btnLeft10 = FindViewById<Button>(Resource.Id.buttonLeft10);
            var btnRight10 = FindViewById<Button>(Resource.Id.buttonRight10);

            var btnPrev = FindViewById<Button>(Resource.Id.buttonPrevious);
            var btnNext = FindViewById<Button>(Resource.Id.buttonNext);

            var btnVolDown = FindViewById<Button>(Resource.Id.buttonVolumeDown);
            var btnVolUp = FindViewById<Button>(Resource.Id.buttonVolumeUp);

            var btnMute = FindViewById<Button>(Resource.Id.buttonMute);
            var btnPlay = FindViewById<Button>(Resource.Id.buttonPlayPause);

            var btnSave = FindViewById<Button>(Resource.Id.buttonSaveFile);
            var btnLoad = FindViewById<Button>(Resource.Id.buttonLoadFile);

            var btnHibernate = FindViewById<Button>(Resource.Id.buttonServerHibernate);
            var btnWeakup = FindViewById<Button>(Resource.Id.buttonServerWeakup);

          //  var btnbatteryCheck = FindViewById<Button>(Resource.Id.batteryCheck);

           var checkBoxBattery = FindViewById<CheckBox>(Resource.Id.checkBoxBattery);

            

            btnLeft.Click += delegate { clientView.SendLeft(); };
            btnRight.Click += delegate { clientView.SendRight(); };

            btnLeft10.Click += delegate { clientView.SendLeft(edit.Text); };
            btnRight10.Click += delegate { clientView.SendRight(edit.Text); };

            btnPrev.Click += delegate { clientView.SendPageUp(); };
            btnNext.Click += delegate { clientView.SendPageDown(); };

            btnVolDown.Click += delegate { clientView.SendVolumeL(); };
            btnVolUp.Click += delegate { clientView.SendVolumeH(); };

            btnMute.Click += delegate { clientView.SendVolumeMute(); };
            btnPlay.Click += delegate { clientView.SendSpace(); };

            btnSave.Click += delegate { clientView.SendSaveName(); };
            btnLoad.Click += delegate { clientView.SendLoadMovie(); };

            btnHibernate.Click += delegate { clientView.SendHibernate(edit.Text); };
            btnWeakup.Click += delegate { clientView.SendWol(); };

            var ifoView = FindViewById<TextView>(Resource.Id.ifoView);
            BatteryAlarm.ifoView = ifoView;

            //btnbatteryCheck.Click += delegate { 
            //    BatteryAlarm.Register();                
            //};
            //btnbatteryCheck.Click += (s, arg) =>
            //{
            //    //PopupMenu menu = new PopupMenu(this, btnbatteryCheck);
            //    //menu.Inflate(Resource.Menu.popMenu);
            //    //menu.Show();
            //    //menu.MenuItemClick += Menu_MenuItemClick;
            //};

            DemoService.activity = this;
            checkBoxBattery.CheckedChange += CheckBoxBattery_CheckedChange;
        }

        private void CheckBoxBattery_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (e.IsChecked)
            {
                StartService();
            }
            else
            {
                StopService();
            }
        }

        private void Menu_MenuItemClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            if (e.Item.ItemId == Resource.Id.new_game1)
            {
                
            }

            if (e.Item.ItemId == Resource.Id.help)
            {
                throw new NotImplementedException();
            }

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private void CreateButtonHandler()
        {
            //var bntOk = FindViewById<Button>(Resource.Id.button1);
            //bntOk.Click += delegate
            //{
            //    var alert = new AndroidX.AppCompat.App.AlertDialog.Builder(this);
            //    alert.SetTitle(GetString(Resource.String.attention));
            //    alert.SetMessage(GetString(Resource.String.attention_message));
            //    alert.SetCancelable(false);
            //    alert.SetPositiveButton(GetString(Resource.String.yes), delegate { Java.Lang.JavaSystem.Exit(0); });
            //    alert.SetNegativeButton(GetString(Resource.String.no), delegate { });

            //    RunOnUiThread(() =>
            //    {
            //        alert.Show();
            //    }
            //    );
            //};
        }
        private void StartService()
        {
            Intent intent = new Intent(this, typeof(DemoService));
            this.StartService(intent);
        }
        private void StopService()
        {
            Intent intent = new Intent(this, typeof(DemoService));
            this.StopService(intent);
        }

    }
}