
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniAspNetCoreMvc.MiniMvc
{
    public class ControllerActionEndpointDataSource : ActionEndpointDataSourceBase
    {
        private readonly List<ConventionalRouteEntry> _conventionalRoutes;
        private int _order;
        private readonly RoutePatternTransformer _routePatternTransformer;
        private readonly RequestDelegate _requestDelegate;

        public ControllerActionEndpointConventionBuilder DefaultBuilder { get; }

        public ControllerActionEndpointDataSource(IActionDescriptorCollectionProvider provider, RoutePatternTransformer transformer) : base(provider)
        {
            _conventionalRoutes = new List<ConventionalRouteEntry>();
            _order = 0;
            _routePatternTransformer = transformer;
            _requestDelegate = ProcessRequestAsync;
            DefaultBuilder = new ControllerActionEndpointConventionBuilder(base.Conventions);
        }

        public ControllerActionEndpointConventionBuilder AddRoute(string routeName, string pattern, RouteValueDictionary defaults, IDictionary<string, object> constraints, RouteValueDictionary dataTokens)
        {
            List<Action<EndpointBuilder>> conventions = new List<Action<EndpointBuilder>>();
            _order++;
            _conventionalRoutes.Add(new ConventionalRouteEntry(routeName, pattern, defaults, constraints, dataTokens, _order, conventions));
            return new ControllerActionEndpointConventionBuilder(conventions);
        }

        protected override List<Endpoint> CreateEndpoints(IReadOnlyList<ActionDescriptor> actions, IReadOnlyList<Action<EndpointBuilder>> conventions)
        {
            var endpoints = new List<Endpoint>();
            foreach (var action in actions)
            {
                var attributeInfo = action.AttributeRouteInfo;
                if (attributeInfo == null) //约定路由
                {
                    foreach (var route in _conventionalRoutes)
                    {
                        var pattern = _routePatternTransformer.SubstituteRequiredValues(route.Pattern, action.RouteValues);
                        if (pattern != null)
                        {
                            var builder = new RouteEndpointBuilder(_requestDelegate, pattern, route.Order);
                            builder.Metadata.Add(action);
                            endpoints.Add(builder.Build());
                        }
                    }
                }
                else //特性路由
                {
                    var original = RoutePatternFactory.Parse(attributeInfo.Template);
                    var pattern = _routePatternTransformer.SubstituteRequiredValues(original, action.RouteValues);
                    if (pattern != null)
                    {
                        var builder = new RouteEndpointBuilder(_requestDelegate, pattern, attributeInfo.Order);
                        builder.Metadata.Add(action);
                        endpoints.Add(builder.Build());
                    }
                }
            }
            return endpoints;
        }

        private Task ProcessRequestAsync(HttpContext httContext)
        {
            var endpoint = httContext.GetEndpoint();
            var actionDescriptor = endpoint.Metadata.GetMetadata<ActionDescriptor>();
            var actionContext = new ActionContext
            {
                ActionDescriptor = actionDescriptor,
                HttpContext = httContext
            };

            var invokerFactory = httContext.RequestServices.GetRequiredService<IActionInvokerFactory>();
            var invoker = invokerFactory.CreateInvoker(actionContext);
            return invoker.InvokeAsync();
        }
    }


    internal struct ConventionalRouteEntry
    {
        public string RouteName;
        public RoutePattern Pattern { get; }
        public RouteValueDictionary DataTokens { get; }
        public int Order { get; }
        public IReadOnlyList<Action<EndpointBuilder>> Conventions { get; }

        public ConventionalRouteEntry(string routeName, string pattern,
            RouteValueDictionary defaults, IDictionary<string, object> constraints,
            RouteValueDictionary dataTokens, int order,
            List<Action<EndpointBuilder>> conventions)
        {
            RouteName = routeName;
            DataTokens = dataTokens;
            Order = order;
            Conventions = conventions;
            Pattern = RoutePatternFactory.Parse(pattern, defaults, constraints);
        }
    }

    public class ControllerActionEndpointConventionBuilder : IEndpointConventionBuilder
    {
        private readonly List<Action<EndpointBuilder>> _conventions;
        public ControllerActionEndpointConventionBuilder(List<Action<EndpointBuilder>> conventions)
        {
            _conventions = conventions;
        }
        public void Add(Action<EndpointBuilder> convention) => _conventions.Add(convention);
    }
}
