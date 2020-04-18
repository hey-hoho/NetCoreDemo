using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //var demo = new CatFramework.Demo();
            //demo.Run();
            //var s = Environment.GetEnvironmentVariables();
            //var ss = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.FirstOrDefault(address => address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.ToString();

            //ScoreRank.TreeTester.Run();

            //new MiniAspNet.Tester().Run();

            new AotuPlugin.PluginControl().LoadPlugin();

            //ConcurrentQueue<int> queue = new System.Collections.Concurrent.ConcurrentQueue<int>();
            //for (int i = 0; i < 100000; i++)
            //{
            //    queue.Enqueue(i);
            //}
            //int[] arry = queue.ToArray();
            //queue.Enqueue(31231);
            //Console.WriteLine(queue.Count());

            try
            {
                //JObject parseObj = JsonConvert.DeserializeObject("{\"first\":\"1\",\"second\":\"2\",\"xxoo\":\"xxxxoooo\"}") as JObject;
                //dynamic blogPost = JsonConvert.DeserializeObject<dynamic>("{\"first\":\"1\",\"second\":\"2\",\"xxoo\":\"xxxxoooo\"}");
                //var s = blogPost["first"];
                //var i= Convert.ChangeType(s, typeof(int));
                // var value = parseObj.GetType().GetField("first").GetValue(parseObj);
                //return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception ex)
            {
                //return default;
            }

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
