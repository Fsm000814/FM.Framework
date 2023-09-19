using Abp.Modules;
using Abp;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using Abp.AspNetCore;

namespace FM.FrameWork.Web.Host.Startup
{
    /// <summary>
    /// AbpEx服务集合扩展
    /// </summary>
    public static class FMFrameWorkAbpExServiceCollectionExtensions
    {
        /// <summary>
        /// 将 FMFrameWorkAbpEx 添加到服务集合，用于配置 ABP（ASP.NET Boilerplate）框架
        /// </summary>
        /// <typeparam name="TStartupModule">启动模块类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="optionsAction">配置选项的操作</param>
        /// <param name="removeConventionalInterceptors">是否移除传统拦截器</param>
        /// <returns>已配置的服务提供程序</returns>
        public static IServiceProvider AddFMFrameWorkAbpEx<TStartupModule>(this IServiceCollection services, Action<FMFrameWorkAbpBootstrapperOptions> optionsAction = null, bool removeConventionalInterceptors = true)
            where TStartupModule : AbpModule
        {
            // 创建一个 Abp引导程序选项 实例
            FMFrameWorkAbpBootstrapperOptions frameWorkAbpBootstrapperOptions = new FMFrameWorkAbpBootstrapperOptions();

            // 执行传入的 optionsAction 方法，并将 Abp引导程序选项实例 作为参数传递
            optionsAction?.Invoke(frameWorkAbpBootstrapperOptions);

            // 将 ABP（ASP.NET Boilerplate）框架添加到服务集合中，并根据选项决定是否移除传统拦截器
            services.AddAbpWithoutCreatingServiceProvider<TStartupModule>(frameWorkAbpBootstrapperOptions.AbpOptionsAction, removeConventionalInterceptors);

            // 获取单例的 AbpBootstrapper 服务，如果不存在则为 null
            AbpBootstrapper singletonServiceOrNull = services.GetSingletonServiceOrNull<AbpBootstrapper>();

            // 执行 frameWorkAbpBootstrapperOptions.AbpConfigureAspNetCoreAfter 方法，用于在 ASP.NET Core 中配置 ABP
            frameWorkAbpBootstrapperOptions?.AbpConfigureAspNetCoreAfter?.Invoke(services);

            // 使用 WindsorRegistrationHelper.CreateServiceProvider 方法基于 IocContainer 和服务集合创建服务提供程序
            return WindsorRegistrationHelper.CreateServiceProvider(singletonServiceOrNull.IocManager.IocContainer, services);
        }
    }
}
