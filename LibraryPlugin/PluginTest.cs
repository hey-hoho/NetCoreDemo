using Microsoft.Extensions.Configuration.Json;
using System;

namespace LibraryPlugin
{
    public class PluginTest
    {
        public void SayHello()
        {
            var Configuration = new Microsoft.Extensions.Configuration.ConfigurationBuilder().
                Add(new JsonConfigurationSource { Path = "testsettings.json", Optional = false, ReloadOnChange = true }).Build();

            Console.WriteLine($"{new LibraryTemplate.Class1().GetAssemblyName()}：hello~");
            Console.WriteLine($"{Configuration["MyKey"]}");
        }
    }
}
