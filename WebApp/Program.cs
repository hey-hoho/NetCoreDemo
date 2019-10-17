using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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

            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseConfiguration(config);
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel();
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
