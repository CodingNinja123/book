using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter11.IoC.Example
{
    public class IoCSimpleExampleMain
    {
        public static void Main()
        {
            var container = new ServiceCollection();
            container.AddScoped<IGenericDependency, GenericDependency>();
            container.AddScoped<GenericService>();

            // Build the IoC and get a provider
            var provider = container.BuildServiceProvider();
            var service = provider.GetService<GenericService>();
            service.DoSomeCoolStuff();
        }
    }
}
