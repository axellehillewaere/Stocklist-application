using EE.Hillewaere.Domain.Services;
using EE.Hillewaere.Views;
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
        private INavigation navigation;

        public MainViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }

        public ICommand ViewPlaceNewOrder => new Command(
            async () =>
            {
                await navigation.PushAsync(new OrderView());
            });

        public ICommand ViewOverviewStocklist => new Command(
            async () =>
            {
                await navigation.PushAsync(new StocklistView());
            });

        public ICommand ViewPreviousOrders => new Command(
            async () =>
            {
                await navigation.PushAsync(new PreviousOrdersView());
            });
    }
}
