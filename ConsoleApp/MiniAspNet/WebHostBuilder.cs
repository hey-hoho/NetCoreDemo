using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.MiniAspNet
{
    public class WebHostBuilder
    {
        private IServer _server;

        private readonly List<Action<IApplicationBuilder>> _configures = new List<Action<IApplicationBuilder>>();

        public WebHostBuilder Configuretion(Action<IApplicationBuilder> configure)
        {
            _configures.Add(configure);
            return this;
        }

        public WebHostBuilder UseServer(IServer server)
        {
            _server = server;
            return this;
        }

        public IWebHost Build()
        {
            var builder = new ApplicationBuilder();
            foreach (var item in _configures)
            {
                item(builder);
            }
            return new WebHost(_server, builder.Build());
        }
    }
}
