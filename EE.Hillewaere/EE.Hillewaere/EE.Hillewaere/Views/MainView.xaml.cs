using Autofac;
using EE.Hillewaere.Domain.Services;
using EE.Hillewaere.IoC;
using EE.Hillewaere.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EE.Hillewaere.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainView : ContentPage
	{
        StocklistInMemoryService stocklistService;

		public MainView ()
		{
			InitializeComponent ();
            BindingContext = IoCRegistry.Container.Resolve<MainViewModel>(
                new NamedParameter("navigation", this.Navigation));
		}
    }
}