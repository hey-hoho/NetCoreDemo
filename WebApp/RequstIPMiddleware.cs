using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public class RequstIPMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequstIPMiddleware(RequestDelegate next, ILogger<RequstIPMiddleware> loggerFactory)
        {
            _next = next;
            _logger = loggerFactory;
        }

        public async Task Invoke(HttpContext context, IWebHostEnvironment env)
        {
            _logger.LogInformation("User IP:" + context.Connection.RemoteIpAddress.ToString());
            await _next.Invoke(context);
        }
    }

    public static class RequestIPExtension
    {
        public static IApplicationBuilder UseRequestIP(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequstIPMiddleware>();
        }
    }
}
