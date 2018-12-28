using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EE.Hillewaere.Domain.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(EE.Hillewaere.Droid.Services.PlatformSoundPlayer))]
namespace EE.Hillewaere.Droid.Services
{
    public class PlatformSoundPlayer : ISoundPlayer
    {
        private MediaPlayer _mediaPlayer;

        public Task PlaySound()
        {
            _mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.guitarm);
            _mediaPlayer.Start();
            return Task.Delay(0);
        }
    }
}