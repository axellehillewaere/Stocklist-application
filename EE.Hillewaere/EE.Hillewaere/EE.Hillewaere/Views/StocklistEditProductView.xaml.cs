using Autofac;
using EE.Hillewaere.Domain.Models;
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
	public partial class StocklistEditProductView : ContentPage
	{
		public StocklistEditProductView (Product product)
		{
			InitializeComponent();
            BindingContext = IoCRegistry.Container.Resolve<StocklistEditProductViewModel>(
                new NamedParameter("product", product),
                new NamedParameter("navigation", this.Navigation));
            //IStocklistService slService = IoCRegistry.Container.Resolve<IStocklistService>();
            //BindingContext = new StocklistEditProductViewModel(product, this.Navigation, slService);
		}

        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<StocklistEditProductViewModel, Product>(this, Constants.MessageNames.ProductSaved,
                async (StocklistEditProductViewModel sender, Product savedProduct) =>
                {
                    await DisplayAlert("Saved", $"Your product {savedProduct.Name} has been saved", "Ok");
                });
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<StocklistEditProductViewModel, Product>(this, Constants.MessageNames.ProductSaved);
            base.OnDisappearing();
        }
    }
}