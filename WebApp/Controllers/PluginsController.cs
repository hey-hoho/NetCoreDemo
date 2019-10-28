using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using WebApp.Plugins;

namespace WebApp.Controllers
{
    public class PluginsController : Controller
    {
        private ApplicationPartManager _partManager = null;

        public PluginsController(ApplicationPartManager partManager)
        {
            this._partManager = partManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 动态加载一个插件
        /// </summary>
        /// <returns></returns>
        public IActionResult Enable()
        {
            var assembly = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "Plugins\\DemoPlugin1.dll");
            var viewAssembly = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "Plugins\\DemoPlugin1.Views.dll");
            var viewAssemblyPart = new CompiledRazorAssemblyPart(viewAssembly);

            var controllerAssemblyPart = new AssemblyPart(assembly);
            _partManager.ApplicationParts.Add(controllerAssemblyPart);
            _partManager.ApplicationParts.Add(viewAssemblyPart);

            MyActionDescriptorChangeProvider.Instance.HasChanged = true;
            MyActionDescriptorChangeProvider.Instance.TokenSource.Cancel();

            return Content("Enabled");
        }
    }
}