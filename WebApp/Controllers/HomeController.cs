using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private ILogger<HomeController> _logger;
        private HttpContext _httpContext;
        private IOptions<HostingSettingOption> _hosting;
        private IMemoryCache _cache;

        public HomeController(
            ILogger<HomeController> logger,
            IHttpContextAccessor httpContext,
            IOptions<HostingSettingOption> hosting,
            IMemoryCache cache)
        {
            this._logger = logger;
            this._httpContext = httpContext.HttpContext;
            this._hosting = hosting;
            this._cache = cache;
        }

        public IActionResult Index()
        {
            _logger.LogWarning("asdddddddddddasssssssssssssss=============" + _hosting.Value.Host);
            _cache.Set("ckey", "qweqweqwe", TimeSpan.FromSeconds(5));
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page." + _httpContext.Request.Host.Port;

            return View();
        }

        [Filters.AddHeaderAttribute("author", "@hoho")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page." + _cache.Get<string>("ckey");
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
