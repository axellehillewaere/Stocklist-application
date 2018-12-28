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
		public StocklistView ()
		{
			InitializeComponent ();
            IStocklistService slService = new StocklistInMemoryService();
            BindingContext = new StocklistViewModel(this.Navigation, slService);
            //categoryListService = new CategoriesInMemoryService();
		}
    }
}