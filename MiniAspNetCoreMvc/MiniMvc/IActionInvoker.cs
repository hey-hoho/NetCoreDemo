
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniAspNetCoreMvc.MiniMvc
{
    public class ControllerActionInvoker : IActionInvoker
    {
        public ActionContext ActionContext { get; }
        public ControllerActionInvoker(ActionContext actionContext) => ActionContext = actionContext;
        public Task InvokeAsync()
        {
            var actionDescriptor = (ControllerActionDescriptor)ActionContext.ActionDescriptor;
            var controllerType = actionDescriptor.ControllerType;
            var requestServies = ActionContext.HttpContext.RequestServices;
            var controllerInstance = ActivatorUtilities.CreateInstance(requestServies, controllerType);
            if (controllerInstance is Controller controller)
            {
                controller.ActionContext = ActionContext;
            }
            var actionMethod = actionDescriptor.Method;
            var result = actionMethod.Invoke(controllerInstance, new object[0]);
            return result is Task task ? task : Task.CompletedTask;
        }
    }

    public interface IActionInvoker
    {
        Task InvokeAsync();
    }

    public interface IActionInvokerFactory
    {
        IActionInvoker CreateInvoker(ActionContext actionContext);
    }

    public class ActionInvokerFactory : IActionInvokerFactory
    {
        public IActionInvoker CreateInvoker(ActionContext actionContext) => new ControllerActionInvoker(actionContext);
    }

    public class ActionContext
    {
        public ActionDescriptor ActionDescriptor { get; set; }
        public HttpContext HttpContext { get; set; }
    }
}
