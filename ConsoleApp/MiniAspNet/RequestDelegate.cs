using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.MiniAspNet
{
    /// <summary>
    /// Func<HttpContext，Task>
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public delegate Task RequestDelegate(HttpContext context);
}
