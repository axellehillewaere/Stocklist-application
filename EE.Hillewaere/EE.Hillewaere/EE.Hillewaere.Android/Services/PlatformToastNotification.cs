using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EE.Hillewaere.Domain.Services;

[assembly: Xamarin.Forms.Dependency(typeof(EE.Hillewaere.Droid.Services.PlatformToastNotification))]
namespace EE.Hillewaere.Droid.Services
{
    public class PlatformToastNotification : IToastNotification
    {
        public void Show(string message)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}