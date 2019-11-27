using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ConsoleApp.AotuPlugin
{
    public class PluginControl
    {
        public void LoadPlugin()
        {
            string pluginLocation = "D:\\heao\\0.heao\\github\\NetCoreDemo\\LibraryPlugin\\bin\\Debug\\netcoreapp3.0\\LibraryPlugin.dll";
            PluginLoadContext loadContext = new PluginLoadContext(pluginLocation);
            var assembly = loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
            Type type = assembly.GetType("LibraryPlugin.PluginTest", true, true);
            var instance = Activator.CreateInstance(type);
            var method = type.GetMethod("SayHello");
            method.Invoke(instance, null);
        }
    }
}
