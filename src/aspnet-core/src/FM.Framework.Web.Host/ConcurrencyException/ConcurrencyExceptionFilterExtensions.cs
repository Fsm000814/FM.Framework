using Abp.AspNetCore.Mvc.ExceptionHandling;
using Abp.Localization;

using FM.Framework.Core.App;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Framework.Web.Host.ConcurrencyException
{
    public static class ConcurrencyExceptionFilterExtensions
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
            mvcOptions.Filters.Insert<ConcurrencyExceptionFilter>(num + 1);
            return mvcOptions;
        }

        public static IServiceCollection ConfigConcurrencyException(this IServiceCollection services, Action<ConcurrencyExceptionOptions> configAction)
        {
            ConcurrencyExceptionOptions gctConcurrencyExceptionOptions = new ConcurrencyExceptionOptions();
            configAction?.Invoke(gctConcurrencyExceptionOptions);
            gctConcurrencyExceptionOptions.ConcurrencyMessage = gctConcurrencyExceptionOptions.ConcurrencyMessage ?? new LocalizableString("ConcurrencyExceptionMessage", AppConsts.LocalizationSourceName);
            services.AddSingleton(gctConcurrencyExceptionOptions);
            return services;
        }

       
    }
}
