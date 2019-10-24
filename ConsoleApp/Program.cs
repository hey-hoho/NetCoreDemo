using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var demo = new CatFramework.Demo();
            demo.Run();

            //var serviceCollection = new ServiceCollection();
            //serviceCollection.AddLogging((config) =>
            //{
            //    config.AddConsole();
            //});

            //serviceCollection.AddSingleton<IFooService, FooService>();
            //serviceCollection.AddSingleton<IBarService, BarService>();

            //var serviceProvider = serviceCollection.BuildServiceProvider();
            //var bar = serviceProvider.GetService<IBarService>();
            //bar.DoSomeRealWork();

            Console.ReadKey();
        }
    }
}
