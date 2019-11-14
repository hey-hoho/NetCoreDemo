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

            //●Singleton服务实例保存在作为根容器的IServiceProvider对象上，所以它能够在多个同根IServiceProvider对象之间提供真正的单例保证。
            //●Scoped服务实例被保存在当前IServiceProvider对象上，所以它只能在当前范围内保证提供的实例是单例的。
            //●没有实现IDisposable接口的Transient服务则采用“即用即建，用后即弃”的策略。
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
