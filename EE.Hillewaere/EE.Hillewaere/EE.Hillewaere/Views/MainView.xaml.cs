using EE.Hillewaere.Domain.Services;
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
            stocklistService = new StocklistInMemoryService();
		}

        private async void btnPlaceNewOrder_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrderView());
        }

        private async void btnOverviewStocklist_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StocklistView());
        }

        private async void btnPreviousOrders_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PreviousOrdersView());
        }
    }
}