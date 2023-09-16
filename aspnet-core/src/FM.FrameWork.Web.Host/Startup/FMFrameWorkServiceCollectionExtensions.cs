using Abp.AspNetCore;
using Abp;
using Abp.Modules;

using Castle.Windsor.MsDependencyInjection;

using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Mvc;

namespace FM.FrameWork.Web.Host.Startup
{
    public static class FMFrameWorkServiceCollectionExtensions
    {
        /// <summary>
        /// 将FMFrameWork 集成到Asp.Net Core
        /// </summary>
        /// <typeparam name="TStartupModule"></typeparam>
        /// <param name="services">服务注册器</param>
        /// <param name="optionsAction">配置选项</param>
        /// <param name="removeConventionalInterceptors">移除常规的拦截器</param>
        /// <returns></returns>
        public static IServiceProvider AddFMFrameWorkAbp<TStartupModule>(this IServiceCollection services, Action<FMFrameWorkAbpBootstrapperOptions> optionsAction = null, bool removeConventionalInterceptors = true) where TStartupModule : AbpModule
        {

            //services.AddAspNetCoreLicense();
            return services.AddFMFrameWorkAbpEx<TStartupModule>(delegate (FMFrameWorkAbpBootstrapperOptions options)
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
