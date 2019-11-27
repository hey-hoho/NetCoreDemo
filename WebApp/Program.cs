using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("hosting.json", optional: true, reloadOnChange: true)
                    .Build();

            //CreateDefaultBuilder执行下列任务：
            //●使用应用程序的托管配置提供应用程序将Kestrel服务器配置为Web服务器。
            //●将内容根设置为由 Directory.GetCurrentDirectory返回的路径。
            //●通过以下对象加载主机配置：
            //○前缀为ASPNETCORE_的环境变量（例如，ASPNETCORE_ENVIRONMENT）。
            //○命令行参数。
            //●按以下顺序加载应用程序配置：
            //○appsettings.json。
            //○appsettings.{ Environment}.json。
            //○应用在使用入口程序集的Development环境中运行时的机密管理器。
            //○环境变量。
            //○命令行参数。
            //●配置控制台和调试输出的日志记录。日志记录包含appsettings.json或appsettings.{ Environment}.json文件的日志记录配置部分中指定的日志筛选规则。
            //●使用ASP.NET Core模块在IIS后面运行时，CreateDefaultBuilder会启用IIS集成，这会配置应用程序的基址和端口。IIS集成还配置应用程序以捕获启动错误。
            //●如果应用环境为“开发（Development）”，请将ServiceProviderOptions.ValidateScopes设为true。
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseConfiguration(config);
                    webBuilder.UseStartup<Startup>();
                    //webBuilder.UseSetting(WebHostDefaults.ApplicationKey, "CoreWeb");
                    webBuilder.ConfigureKestrel((context, options) =>
                    {
                        options.Limits.MaxRequestBodySize = 20000000;
                    });
                    webBuilder.UseSerilog((context, configuration) =>
                    {
                        configuration
                            .MinimumLevel.Information()
                            // 日志调用类命名空间如果以 Microsoft 开头，覆盖日志输出最小级别为 Information
                            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
                            .Enrich.FromLogContext()
                            // 配置日志输出到控制台
                            //.WriteTo.Console()
                            // 配置日志输出到文件，文件输出到当前项目的 logs 目录下
                            // 日记的生成周期为每天
                            .WriteTo.File(Path.Combine("logs", @"log.txt"), rollingInterval: RollingInterval.Day);
                        // 创建 logger
                        //.CreateLogger();
                    });
                });
        }
    }
}
