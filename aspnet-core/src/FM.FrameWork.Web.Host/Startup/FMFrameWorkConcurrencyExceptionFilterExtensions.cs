using Abp.AspNetCore.Mvc.ExceptionHandling;
using Abp.Localization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FM.FrameWork.Web.Host.Startup
{
    public static class FMFrameWorkConcurrencyExceptionFilterExtensions
    {
        //
        // 摘要:
        //     添加并发异常Filter
        //
        // 参数:
        //   mvcOptions:
        public static MvcOptions AddConcurrencyExceptionFilter(this MvcOptions mvcOptions)
        {
            int num = mvcOptions.Filters.IndexOf<AbpExceptionFilter>();
            mvcOptions.Filters.Insert<FMFrameWorkConcurrencyExceptionFilter>(num + 1);
            return mvcOptions;
        }

        public static IServiceCollection ConfigConcurrencyException(this IServiceCollection services, Action<FMFrameWorkConcurrencyExceptionOptions> configAction)
        {
            FMFrameWorkConcurrencyExceptionOptions gctConcurrencyExceptionOptions = new FMFrameWorkConcurrencyExceptionOptions();
            configAction?.Invoke(gctConcurrencyExceptionOptions);
            gctConcurrencyExceptionOptions.ConcurrencyMessage = gctConcurrencyExceptionOptions.ConcurrencyMessage ?? new LocalizableString("ConcurrencyExceptionMessage", FMFrameWorkConsts.LocalizationSourceName);
            services.AddSingleton(gctConcurrencyExceptionOptions);
            return services;
        }
    }
}
