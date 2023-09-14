using Abp.Dependency;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;

using Serilog;

using System;
using System.Text.Json;

namespace FM.Framework.FMFrameWork
{
    public class TrackExceptionFilter : IExceptionFilter, ITransientDependency
    {
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly ILogger _Logger;


        public TrackExceptionFilter(
            IHostApplicationLifetime applicationLifetime,
            ILogger logger)
        {
            _applicationLifetime = applicationLifetime;
            _Logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is OutOfMemoryException outOfMemoryException)
            {
                var ex = JsonSerializer.Serialize(outOfMemoryException.StackTrace);
                //Console.WriteLine(outOfMemoryException.Message);
                _Logger.Error(ex);
                Console.WriteLine("内存溢出异常，停止应用");
                _applicationLifetime.StopApplication();
                //Process.GetCurrentProcess().Kill();
            }
        }
    }
}

namespace Microsoft.AspNetCore.Mvc.FMFramework
{
    using Abp.AspNetCore.Mvc.ExceptionHandling;
    using FM.Framework.FMFrameWork;
    using FM.Framework.Web.Host;
    using System;

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

            mvcOptions.Filters.Insert<TrackExceptionFilter>(abpExceptionFilterIndex + 1);

            return mvcOptions;
        }
    }
}