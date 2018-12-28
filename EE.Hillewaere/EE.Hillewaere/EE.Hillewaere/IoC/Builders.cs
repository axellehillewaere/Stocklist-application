using Autofac;
using EE.Hillewaere.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EE.Hillewaere.IoC
{
    public static class Builders
    {
        public static ContainerBuilder GetDefaultContainerBuilder()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<StocklistInMemoryService>().As<IStocklistService>();
            return containerBuilder;
        }
    }
}
