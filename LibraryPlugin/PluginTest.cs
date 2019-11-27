using Microsoft.Extensions.Configuration.Json;
using System;

namespace LibraryPlugin
{
    public class PluginTest
    {
        public void SayHello()
        {
            //var Configuration = new Microsoft.Extensions.Configuration.ConfigurationBuilder().
            //    Add(new JsonConfigurationSource { Path = "appsettings.json", Optional = false, ReloadOnChange = true }).Build();

            Console.WriteLine($"{new LibraryTemplate.Class1().GetAssemblyName()}：hello~");
            Console.WriteLine($"{System.Configuration.ConfigurationManager.AppSettings["MyKey"]}");
        }
    }
}
