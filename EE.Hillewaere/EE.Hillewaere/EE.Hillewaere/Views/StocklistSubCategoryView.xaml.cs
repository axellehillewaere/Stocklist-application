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
	public partial class StocklistSubCategoryView : ContentPage
	{
        private CategoriesInMemoryService categoryService;
        private Category currentCategory;

		public StocklistSubCategoryView (Category category)
		{
			InitializeComponent ();
            categoryService = new CategoriesInMemoryService();

            if (category == null)
            {
                currentCategory = new Category();
                Title = "New Category List";
            }
            else
            {
                currentCategory = category;
                Title = currentCategory.Name;
            }
		}
	}
}