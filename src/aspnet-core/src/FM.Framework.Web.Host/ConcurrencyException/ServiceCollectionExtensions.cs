using Abp.Modules;
using FM.Framework.Web.Host.ConcurrencyException;
using FMFramework.AbpEx;
using FMFramework.AbpEX;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FM.Framework.Web.Host
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 将 FMFramework 集成到 Asp.Net Core：
        /// 接受一个泛型参数TStartupModule，该参数限制为继承自AbpModule的类型。
        /// 接受两个可选参数：optionsAction和removeConventionalInterceptors
        /// </summary>
        /// <typeparam name="TStartupModule"></typeparam>
        /// <param name="services">服务注册器</param>
        /// <param name="optionsAction">配置选项</param>
        /// <param name="removeConventionalInterceptors">移除常规的拦截器</param>
        /// <returns></returns>
        public static IServiceProvider AddFMFrameworkAbp<TStartupModule>(this IServiceCollection services, Action<FMFrameworkAbpExBootstrapperOptions> optionsAction = null, bool removeConventionalInterceptors = true) where TStartupModule : AbpModule
        {
            //添加许可证服务
            //services.AddAspNetCoreLicense(); 

            //调用AddFMFrameworkAbpEx<TStartupModule>方法来添加FMFrameworkAbpEx服务，传递了一个委托作为参数
            return services.AddFMFrameworkAbpEx<TStartupModule>(delegate (FMFrameworkAbpExBootstrapperOptions options)
            {
                //首先通过执行optionsAction?.Invoke(options)来调用传入的optionsAction方法，
                //以便可以对FMFrameworkAbpExBootstrapperOptions进行自定义配置。
                optionsAction?.Invoke(options);
                Action<IServiceCollection> customAbpConfigureAspNetCoreAfter = null;
                //将options.AbpConfigureAspNetCoreAfter重新赋值为一个新的委托，该委托在执行时会执行一些额外的操作。
                if (options.AbpConfigureAspNetCoreAfter != null)
                {
                    customAbpConfigureAspNetCoreAfter = options.AbpConfigureAspNetCoreAfter;
                }

                //其中，通过afterServices.Configure方法向MvcOptions中添加了一个并发异常过滤器。
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
