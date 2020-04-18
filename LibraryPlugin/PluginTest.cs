using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace LibraryPlugin
{
    public class PluginTest
    {
        IServiceProvider serviceProvider;
        public PluginTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<LibraryTemplate.ITemplateService, LibraryTemplate.TemplateService>();
            serviceCollection.AddTransient<LibraryTemplate.ITemplateService2, LibraryTemplate.TemplateService2>();
            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public void SayHello()
        {
            //var Configuration = new Microsoft.Extensions.Configuration.ConfigurationBuilder().
            //    Add(new JsonConfigurationSource
            //    {
            //        Path = @"D:\heao\0.heao\github\NetCoreDemo\LibraryPlugin\testsettings.json",
            //        Optional = false,
            //        ReloadOnChange = true
            //    }).Build();
            var builder = new ConfigurationBuilder()
                         //.SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile(@"D:\heao\0.heao\github\NetCoreDemo\LibraryPlugin\testsettings.json", optional: false, reloadOnChange: true).Build();


            Console.WriteLine($"{new LibraryTemplate.Class1(serviceProvider.GetService<LibraryTemplate.ITemplateService>()).GetAssemblyName()}：hello~");
            Console.WriteLine($"{builder["MyKey"]}");
        }
    }
}
