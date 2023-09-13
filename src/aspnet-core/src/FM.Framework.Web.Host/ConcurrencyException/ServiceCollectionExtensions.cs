using Abp.Modules;
using FM.Framework.Web.Host.ConcurrencyException;

using FMFramework.AbpEX;

using FMFranework.AbpEx;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Framework.Web.Host
{
    public static class ServiceCollectionExtensions
    {
        //
        // 摘要:
        //     将 FMFramework 集成到 Asp.Net Core
        //
        // 参数:
        //   services:
        //     服务注册器
        //
        //   optionsAction:
        //     配置选项
        //
        //   removeConventionalInterceptors:
        //     移除常规的拦截器
        //
        // 类型参数:
        //   TStartupModule:
        public static IServiceProvider AddFMFrameworkAbp<TStartupModule>(this IServiceCollection services, Action<FMFrameworkAbpExBootstrapperOptions> optionsAction = null, bool removeConventionalInterceptors = true) where TStartupModule : AbpModule
        {
            //添加许可证服务
            //services.AddAspNetCoreLicense(); 
            return services.AddFMFrameworkAbpEx<TStartupModule>(delegate (FMFrameworkAbpExBootstrapperOptions options)
            {
                optionsAction?.Invoke(options);
                Action<IServiceCollection> customAbpConfigureAspNetCoreAfter = null;
                if (options.AbpConfigureAspNetCoreAfter != null)
                {
                    customAbpConfigureAspNetCoreAfter = options.AbpConfigureAspNetCoreAfter;
                }

                options.AbpConfigureAspNetCoreAfter = delegate (IServiceCollection afterServices)
                {
                    afterServices.Configure(delegate (MvcOptions mvcOptions)
                    {
                        mvcOptions.AddConcurrencyExceptionFilter();
                    });
                    customAbpConfigureAspNetCoreAfter?.Invoke(afterServices);
                };
            }, removeConventionalInterceptors);
        }
    }
}
