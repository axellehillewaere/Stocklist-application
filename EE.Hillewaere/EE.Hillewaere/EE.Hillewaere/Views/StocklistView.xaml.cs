using EE.Hillewaere.Domain.Models;
using EE.Hillewaere.Domain.Services;
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
	public partial class StocklistView : ContentPage
	{
        StocklistInMemoryService stocklistService;
		public StocklistView ()
		{
			InitializeComponent ();
            BindingContext = new StocklistViewModel(this.Navigation);
            //categoryListService = new CategoriesInMemoryService();
		}

        private async void mnuCategoryEdit_Clicked(object sender, EventArgs e)
        {
            var selectedCategory = ((MenuItem)sender).CommandParameter as Category;
            await DisplayAlert("Edit", $"Editing {selectedCategory.Name}", "OK");
            //await Navigation.PushAsync(new StocklistSubCategoryView());
        }

        private async void mnuCategoryDelete_Clicked(object sender, EventArgs e)
        {
            var selectedCategory = ((MenuItem)sender).CommandParameter as Category;
            await stocklistService.DeleteCategoryList(selectedCategory.Id);
            //await RefreshCategoryLists();
        }
    }
}