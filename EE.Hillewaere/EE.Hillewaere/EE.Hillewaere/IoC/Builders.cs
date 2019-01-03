using Autofac;
using EE.Hillewaere.Domain.Services;
using EE.Hillewaere.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EE.Hillewaere.IoC
{
    public static class Builders
    {
        public static ContainerBuilder GetDefaultContainerBuilder()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<MainViewModel>();
            containerBuilder.RegisterType<OrderViewModel>();
            containerBuilder.RegisterType<OrderSubCategoryViewModel>();
            containerBuilder.RegisterType<OrderProductViewModel>();
            containerBuilder.RegisterType<OrderListViewModel>();
            containerBuilder.RegisterType<StocklistViewModel>();
            containerBuilder.RegisterType<StocklistSubCategoryViewModel>();
            containerBuilder.RegisterType<StocklistProductViewModel>();
            containerBuilder.RegisterType<StocklistEditProductViewModel>();
            containerBuilder.RegisterType<PreviousOrdersViewModel>();
            containerBuilder.RegisterType<StocklistInMemoryService>().As<IStocklistService>();
            return containerBuilder;
        }
    }
}
