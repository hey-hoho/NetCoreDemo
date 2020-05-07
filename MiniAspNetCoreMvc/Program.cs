using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniAspNetCoreMvc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            new MiniMvc.Tester().Run();
        }
    }

    public class HomeController : MiniMvc.Controller
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions { WriteIndented = true };

        [HttpGet()]
        public Task Action() => ActionContext.HttpContext.Response.WriteAsync("hello mini aspnetcore mvc..");

        public string Action1(string foo, int bar, double baz)
        => JsonSerializer.Serialize(new { Foo = foo, Bar = bar, Baz = baz }, _options);

        public string Action2(Foobarbaz value1, Foobarbaz value2)
        => JsonSerializer.Serialize(new { Value1 = value1, Value2 = value2 }, _options);

        public string Action3(Foobarbaz value1, [FromBody]Foobarbaz value2)
        => JsonSerializer.Serialize(new { Value1 = value1, Value2 = value2 }, _options);
    }

    public class Foobarbaz
    {
        public Foobar Foobar { get; set; }
        public double Baz { get; set; }
    }

    public class Foobar
    {
        public string Foo { get; set; }
        public int Bar { get; set; }
    }
}
