﻿using Autofac;
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
	public partial class OrderView : ContentPage
	{
		public OrderView ()
		{
			InitializeComponent ();
            BindingContext = IoCRegistry.Container.Resolve<OrderViewModel>(new NamedParameter("navigation", this.Navigation));
		}
	}
}