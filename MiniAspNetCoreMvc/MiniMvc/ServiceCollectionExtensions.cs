using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniAspNetCoreMvc.MiniMvc
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMvcControllers(this IServiceCollection services)
        {
            return services
                .AddSingleton<IActionDescriptorCollectionProvider, DefaultActionDescriptorCollectionProvider>()
                .AddSingleton<IActionInvokerFactory, ActionInvokerFactory>()
                .AddSingleton<IActionDescriptorProvider, ControllerActionDescriptorProvider>()
                .AddSingleton<ControllerActionEndpointDataSource, ControllerActionEndpointDataSource>();
        }


        public static ControllerActionEndpointConventionBuilder MapMvcControllers(this IEndpointRouteBuilder endpointBuilder)
        {
            var endpointDatasource = endpointBuilder.ServiceProvider.GetRequiredService<ControllerActionEndpointDataSource>();
            endpointBuilder.DataSources.Add(endpointDatasource);
            return endpointDatasource.DefaultBuilder;
        }

        public static ControllerActionEndpointConventionBuilder MapMvcControllerRoute(
            this IEndpointRouteBuilder endpointBuilder, string name, string pattern,
            RouteValueDictionary defaults = null, RouteValueDictionary constraints = null,
            RouteValueDictionary dataTokens = null)
        {
            var endpointDatasource = endpointBuilder.ServiceProvider.GetRequiredService<ControllerActionEndpointDataSource>();
            endpointBuilder.DataSources.Add(endpointDatasource);
            return endpointDatasource.AddRoute(name, pattern, defaults, constraints, dataTokens);
        }
    }

 
}
