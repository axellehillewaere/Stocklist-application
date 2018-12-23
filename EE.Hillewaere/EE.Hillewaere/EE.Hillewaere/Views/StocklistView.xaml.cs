using EE.Hillewaere.Domain.Models;
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
	public partial class StocklistView : ContentPage
	{
        CategoriesInMemoryService categoryListService;
		public StocklistView ()
		{
			InitializeComponent ();
            categoryListService = new CategoriesInMemoryService();
		}

        private async Task RefreshCategoryLists()
        {
            var categories = await categoryListService.GetCategoryList();
            lvCategoriesLists.ItemsSource = categories;
        }

        protected async override void OnAppearing()
        {
            await RefreshCategoryLists();
            base.OnAppearing();
        }

        private async void lvCategoriesLists_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var category = e.Item as Category;
            if (category != null)
            {
                await DisplayAlert("Tap", $"You tapped {category.Name}", "OK");
                await Navigation.PushAsync(new MainView());
            }
        }
    }
}