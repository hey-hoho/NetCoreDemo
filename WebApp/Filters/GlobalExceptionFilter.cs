using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private IWebHostEnvironment _env;
        private ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(IWebHostEnvironment _env, ILogger<GlobalExceptionFilter> _logger)
        {
            this._env = _env;
            this._logger = _logger;
        }

        public void OnException(ExceptionContext context)
        {
            //如果是自定义异常，则不做处理
            //if (context.Exception.GetType() == typeof(BusException))
            //{
            //}
            //else
            //{

            //}

            //日志入库
            //向负责人发报警邮件，异步
            //向负责人发送报警短信或者报警电话，异步

            Exception ex = context.Exception;
            //这里给系统分配标识，监控异常肯定不止一个系统。
            int sysId = 1;
            //这里获取服务器ip时，需要考虑如果是使用nginx做了负载，这里要兼容负载后的ip，
            //监控了ip方便定位到底是那台服务器出故障了
            string ip = context.HttpContext.Connection.RemoteIpAddress.ToString();

            _logger.LogError($"系统编号：{sysId},主机IP:{ip},堆栈信息：{ex.StackTrace},异常描述：{ex.Message}");
            context.Result = new JsonResult(ex.Message);
            context.ExceptionHandled = true;
        }
    }
}
