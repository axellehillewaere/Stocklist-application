using EE.Hillewaere.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;

[assembly: Dependency(typeof(EE.Hillewaere.UWP.Services.PlatformSoundPlayer))]
namespace EE.Hillewaere.UWP.Services
{
    internal class PlatformSoundPlayer : ISoundPlayer
    {
        public async Task PlaySound()
        {
            MediaElement mysong = new MediaElement();
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("not-bad.mp3");
            var stream = await file.OpenReadAsync();
            mysong.SetSource(stream, file.ContentType);
            mysong.Play();
        }
    }
}
