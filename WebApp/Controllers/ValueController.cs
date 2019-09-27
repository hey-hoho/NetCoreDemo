using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mvcApp.Controllers
{
    /// <summary>
    /// tttttt
    /// </summary>
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/{v:apiVersion}/Value")]
    [ApiController]
    public class ValueController : ControllerBase
    {
        /// <summary>
        /// sdfdsfds
        /// </summary>
        /// <returns></returns>
        [HttpGet, MapToApiVersion("1.0")]
        public IEnumerable<object> Get()
        {
            //using (BloggingContext db=new BloggingContext())
            //{
            //    return db.Blog.ToList();
            //}
            return new string[] { "Value1 from Version 1", "value2 from Version 1" };
        }
    }
    
    [ApiController]
    public class ValueV2Controller : ControllerBase
    {

    }
}