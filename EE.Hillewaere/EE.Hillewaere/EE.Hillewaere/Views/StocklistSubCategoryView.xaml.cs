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
	public partial class StocklistSubCategoryView : ContentPage
	{
		public StocklistSubCategoryView (Category category)
		{
			InitializeComponent ();
            BindingContext = IoCRegistry.Container.Resolve<StocklistSubCategoryViewModel>(
                new NamedParameter("category", category),
                new NamedParameter("navigation", this.Navigation));
            //IStocklistService slService = IoCRegistry.Container.Resolve<IStocklistService>();
            //BindingContext = new StocklistSubCategoryViewModel(category, this.Navigation, slService);
        }
    }
}