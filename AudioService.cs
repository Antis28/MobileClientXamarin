using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileClientXamarin
{
    public class AudioService 
    {
        private MediaPlayer _mediaPlayer;

        public bool PlayFile()
        {
            if (_mediaPlayer == null)
            {
                _mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.alarm);
            }
            _mediaPlayer.SeekTo(0);

            _mediaPlayer.Start();

            return true;
        }

    }
}