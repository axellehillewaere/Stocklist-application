using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace EE.Hillewaere.IoC
{
    public class IoCRegistry
    {
        private static IContainer container;
        public static IContainer Container { get
            {
                if (container == null)
                {
                    var builder = Builders.GetDefaultContainerBuilder();
                    container = builder.Build();
                }
                return container;
            } }
    }
}
