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
	public partial class StocklistSubCategoryView : ContentPage
	{
        private CategoriesInMemoryService categoryService;
        private Category currentCategory;

		public StocklistSubCategoryView (Category subCategory)
		{
			InitializeComponent ();
            BindingContext = new StocklistSubCategoryViewModel();

            categoryService = new CategoriesInMemoryService();

            if (subCategory == null)
            {
                currentCategory = new Category();
                Title = "New Category List";
            }
            else
            {
                currentCategory = subCategory;
                Title = currentCategory.Name;
            }
		}

        private async Task RefreshSubCategoryLists()
        {
            var subCategories = await categoryService.GetSubCategoryList();
            lvSubCategoriesLists.ItemsSource = null;
            lvSubCategoriesLists.ItemsSource = subCategories;
        }

        protected async override void OnAppearing()
        {
            await RefreshSubCategoryLists();
            base.OnAppearing();
        }


        private void lvSubCategoryLists_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var subCategory = e.Item as SubCategory;
        }
    }
}