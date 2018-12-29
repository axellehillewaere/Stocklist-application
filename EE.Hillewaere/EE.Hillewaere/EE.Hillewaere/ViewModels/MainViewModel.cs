using EE.Hillewaere.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EE.Hillewaere.ViewModels
{
    public class MainViewModel
    {
        private ISoundPlayer soundPlayer;
        public ICommand PlaySoundCommand { get; private set; }

        public MainViewModel(ISoundPlayer soundplayer)
        {
            soundPlayer = soundplayer;
            PlaySoundCommand = new Command(PlaySound);
        }

        private async void PlaySound()
        {
            //await DependencyService.Get<ISoundPlayer>().PlaySound();
        }
    }
}
