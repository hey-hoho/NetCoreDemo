using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.Common.Discovery;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private ILogger<HomeController> _logger;
        private HttpContext _httpContext;
        private IOptions<HostingSettingOption> _hosting;
        private IMemoryCache _cache;
        private readonly IDiscoveryClient discoClient;

        public HomeController(
            ILogger<HomeController> logger,
            IHttpContextAccessor httpContext,
            IOptions<HostingSettingOption> hosting,
            IMemoryCache cache,
            IDiscoveryClient discoClient)
        {
            this._logger = logger;
            this._httpContext = httpContext.HttpContext;
            this._hosting = hosting;
            this._cache = cache;
            this.discoClient = discoClient;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Serilog test info.");
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

        public async Task<IActionResult> Weather()
        {
            var apiGatewayInstances = discoClient.GetInstances("api-gateway");
            var apiGatewayUri = apiGatewayInstances.First().Uri;
            using (var client = new HttpClient())
            {
                // 调用计算服务，计算两个整数的和与差
                const int x = 124, y = 134;
                var sumResponse = await client.GetAsync($"{apiGatewayUri}calc/api/calculation/add/{x}/{y}");
                sumResponse.EnsureSuccessStatusCode();
                var sumResult = await sumResponse.Content.ReadAsStringAsync();
                ViewData["sum"] = $"x + y = {sumResult}";

                var subResponse = await client.GetAsync($"{apiGatewayUri}calc/api/calculation/sub/{x}/{y}");
                subResponse.EnsureSuccessStatusCode();
                var subResult = await subResponse.Content.ReadAsStringAsync();
                ViewData["sub"] = $"x - y = {subResult}";

                // 调用天气服务，计算大连和广州的平均气温标准差
                var stddevShenyangResponse = await client.GetAsync($"{apiGatewayUri}weather/api/weather/stddev/shenyang");
                stddevShenyangResponse.EnsureSuccessStatusCode();
                var stddevShenyangResult = await stddevShenyangResponse.Content.ReadAsStringAsync();
                ViewData["stddev_sy"] = $"沈阳：{stddevShenyangResult}";

                var stddevGuangzhouResponse = await client.GetAsync($"{apiGatewayUri}weather/api/weather/stddev/guangzhou");
                stddevGuangzhouResponse.EnsureSuccessStatusCode();
                var stddevGuangzhouResult = await stddevGuangzhouResponse.Content.ReadAsStringAsync();
                ViewData["stddev_gz"] = $"广州：{stddevGuangzhouResult}";

                // 查看Calc服务的App名称
                var calcAppNameResponse = await client.GetAsync($"{apiGatewayUri}calc/api/calculation/info");
                calcAppNameResponse.EnsureSuccessStatusCode();
                var calcAppNameResult = await calcAppNameResponse.Content.ReadAsStringAsync();
                ViewData["calc_app_name"] = $"计算服务名称：{calcAppNameResult}";
            }
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
