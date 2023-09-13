using Abp.Modules;
using Abp;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using FMFramework.AbpEX;
using Abp.AspNetCore;

namespace FMFramework.AbpEx
{
    public static class FMFraneworkAbpExServiceCollectionExtensions
    {
        /// <summary>
        /// 将 FMFraneworkAbpEx 集成到 Asp.Net Core
        /// 封装了添加FMFrameworkAbpEx服务的逻辑，并在内部进行了一些自定义配置，
        /// 例如添加Abp服务和执行AbpConfigureAspNetCoreAfter委托中的操作。
        /// </summary>
        /// <typeparam name="TStartupModule"></typeparam>
        /// <param name="services">服务注册器</param>
        /// <param name="optionsAction">配置选项</param>
        /// <param name="removeConventionalInterceptors">移除常规的拦截器</param>
        /// <returns>IServiceProvider实例</returns>
        public static IServiceProvider AddFMFrameworkAbpEx<TStartupModule>(this IServiceCollection services, Action<FMFrameworkAbpExBootstrapperOptions> optionsAction = null, bool removeConventionalInterceptors = true) where TStartupModule : AbpModule
        {
            //对FMFrameworkAbpExBootstrapperOptions进行自定义配置
            FMFrameworkAbpExBootstrapperOptions fMFraneworkAbp = new FMFrameworkAbpExBootstrapperOptions();
            //依赖注入fMFraneworkAbp
            optionsAction?.Invoke(fMFraneworkAbp);
            //
            services.AddAbpWithoutCreatingServiceProvider<TStartupModule>(fMFraneworkAbp.AbpOptionsAction, removeConventionalInterceptors);
            AbpBootstrapper singletonServiceOrNull = services.GetSingletonServiceOrNull<AbpBootstrapper>();
            fMFraneworkAbp?.AbpConfigureAspNetCoreAfter?.Invoke(services);
            return WindsorRegistrationHelper.CreateServiceProvider(singletonServiceOrNull.IocManager.IocContainer, services);
        }
    }
}