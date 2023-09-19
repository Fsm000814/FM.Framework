using Abp.AspNetCore;
using Abp;
using Abp.Modules;

using Castle.Windsor.MsDependencyInjection;

using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Mvc;

namespace FM.FrameWork.Web.Host.Startup
{
    /// <summary>
    /// 服务集合扩展类
    /// </summary>
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
            // 添加AspNetCoreLicense
            // services.AddAspNetCoreLicense();
            return services.AddFMFrameWorkAbpEx<TStartupModule>(delegate (FMFrameWorkAbpBootstrapperOptions options)
            {
                // 调用传入的optionsAction委托，设置Abp引导程序选项的属性值
                optionsAction?.Invoke(options);

                Action<IServiceCollection> customAbpConfigureAspNetCoreAfter = null;

                // 如果options.AbpConfigureAspNetCoreAfter不为空，则保存到customAbpConfigureAspNetCoreAfter中
                if (options.AbpConfigureAspNetCoreAfter != null)
                {
                    customAbpConfigureAspNetCoreAfter = options.AbpConfigureAspNetCoreAfter;
                }

                // 重新定义options.AbpConfigureAspNetCoreAfter委托
                options.AbpConfigureAspNetCoreAfter = delegate (IServiceCollection afterServices)
                {
                    // 配置MvcOptions，添加并发异常过滤器
                    afterServices.Configure(delegate (MvcOptions mvcOptions)
                    {
                        mvcOptions.AddConcurrencyExceptionFilter();
                    });

                    // 执行customAbpConfigureAspNetCoreAfter委托
                    customAbpConfigureAspNetCoreAfter?.Invoke(afterServices);
                };
            }, removeConventionalInterceptors);
        }
    }
}
