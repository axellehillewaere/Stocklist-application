using Autofac;
using EE.Hillewaere.Domain.Models;
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
	public partial class OrderSubCategoryView : ContentPage
	{
		public OrderSubCategoryView (Category category)
		{
			InitializeComponent ();
            BindingContext = IoCRegistry.Container.Resolve<OrderSubCategoryViewModel>(
                new NamedParameter("category", category),
                new NamedParameter("navigation", this.Navigation));
        }
	}
}