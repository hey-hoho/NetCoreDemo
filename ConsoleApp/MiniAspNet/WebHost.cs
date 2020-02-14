using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.MiniAspNet
{
    public interface IWebHost
    {
        Task RunAsync();
    }

    public class WebHost : IWebHost
    {
        private IServer _server;
        private RequestDelegate _handler;

        public WebHost(IServer server, RequestDelegate handler)
        {
            _server = server;
            _handler = handler;
        }

        public async Task RunAsync()
        {
            await _server.StartAsync(_handler);
        }
    }
}
