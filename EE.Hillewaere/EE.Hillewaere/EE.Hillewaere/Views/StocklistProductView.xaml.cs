﻿using Autofac;
using EE.Hillewaere.Constants;
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
	public partial class StocklistProductView : ContentPage
	{
		public StocklistProductView (SubCategory subCategory)
		{
			InitializeComponent ();
            BindingContext = IoCRegistry.Container.Resolve<StocklistProductViewModel>(
                new NamedParameter("subCategory", subCategory),
                new NamedParameter("navigation", this.Navigation));
        }
    }
}