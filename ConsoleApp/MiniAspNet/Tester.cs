using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.MiniAspNet
{
    public class Tester
    {
        public void Run()
        {
            new WebHostBuilder()
                .UseServer(new HttpListenerServer())
                .Configuretion(app =>
                {
                    app
                    .Use(_ =>
                    {
                        return async context =>
                        {
                            await context.Response.WriteAsync("mini aspnet core :");
                            await _.Invoke(context);
                        };
                    })
                    .Use(_ =>
                    {
                        return async context =>
                        {
                            await context.Response.WriteAsync("hello world...");
                        };
                    });
                })
                .Build()
                .RunAsync();
        }
    }
}
