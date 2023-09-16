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
    public class FMFrameWorkTrackExceptionFilter : IExceptionFilter, ITransientDependency
    {
        private readonly IHostApplicationLifetime _applicationLifetime;

        public FMFrameWorkTrackExceptionFilter(
            IHostApplicationLifetime applicationLifetime
            )
        {
            _applicationLifetime = applicationLifetime;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is OutOfMemoryException outOfMemoryException)
            {
                var ex = JsonSerializer.Serialize(outOfMemoryException.StackTrace);
                Console.WriteLine("内存溢出异常，停止应用");
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
