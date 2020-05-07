using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniAspNetCoreMvc.MiniMvc
{
    class Tester
    {
        public void Run()
        {
            Host.CreateDefaultBuilder()
                 .ConfigureWebHostDefaults(builder => builder
                 .ConfigureServices(services => services
                         .AddRouting()
                         .AddMvcControllers())
                 .Configure(app => app
                 .UseDeveloperExceptionPage()
                     .UseRouting()
                     .UseEndpoints(endpoints => endpoints.MapMvcControllerRoute("default", "{controller}/{action}"))))
                 .Build()
                 .Run();
        }
    }
}
