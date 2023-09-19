using Abp.AspNetCore.Mvc.ExceptionHandling;
using Abp.Dependency;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;

using Serilog;

using System;
using System.Text.Json;

namespace FM.FrameWork.Web.Host.Startup
{
    /// <summary>
    /// 轨迹异常筛选器
    /// </summary>
    public class FMFrameWorkTrackExceptionFilter : IExceptionFilter, ITransientDependency
    {
        private readonly IHostApplicationLifetime _applicationLifetime;

        /// <summary>
        /// 轨迹异常筛选器
        /// </summary>
        /// <param name="applicationLifetime">应用程序生命周期</param>
        public FMFrameWorkTrackExceptionFilter(IHostApplicationLifetime applicationLifetime)
        {
            _applicationLifetime = applicationLifetime;
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context">异常上下文</param>
        public void OnException(ExceptionContext context)
        {
            // 检查是否是 OutOfMemoryException 异常
            if (context.Exception is OutOfMemoryException outOfMemoryException)
            {
                // 序列化堆栈跟踪信息为 JSON 字符串
                var ex = JsonSerializer.Serialize(outOfMemoryException.StackTrace);

                // 打印内存溢出异常消息
                Console.WriteLine("内存溢出异常，停止应用");

                // 停止应用程序
                _applicationLifetime.StopApplication();
            }
        }
    }
    public static class ConcurrencyExceptionFilterExtensions
    {
        /// <summary>
        /// 添加并发异常Filter
        /// </summary>
        /// <param name="mvcOptions"></param>
        /// <returns></returns>
        public static MvcOptions AddTrackExceptionFilter(this MvcOptions mvcOptions)
        {
            var abpExceptionFilterIndex = mvcOptions.Filters.IndexOf<AbpExceptionFilter>();

            mvcOptions.Filters.Insert<FMFrameWorkTrackExceptionFilter>(abpExceptionFilterIndex + 1);

            return mvcOptions;
        }
    }
}
