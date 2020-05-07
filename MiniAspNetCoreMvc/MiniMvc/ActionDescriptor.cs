using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MiniAspNetCoreMvc.MiniMvc
{
    public class ControllerActionDescriptor : ActionDescriptor
    {
        /// <summary>
        /// 所在的Controller类型
        /// </summary>
        public Type ControllerType { get; set; }
        /// <summary>
        /// 表示Action方法本身
        /// </summary>
        public MethodInfo Method { get; set; }
    }

    public abstract class ActionDescriptor
    {
        /// <summary>
        /// 特性路由
        /// </summary>
        public AttributeRouteInfo AttributeRouteInfo { get; set; }
        /// <summary>
        /// 约定路由
        /// </summary>
        public IDictionary<string, string> RouteValues { get; set; }
    }

    public class AttributeRouteInfo
    {
        /// <summary>
        /// 该属性值越小，代表选择优先级越高，来源于对应对象的同名属性（比如HttpGetAttribute）
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 路由模板
        /// </summary>
        public string Template { get; set; }
    }
}
