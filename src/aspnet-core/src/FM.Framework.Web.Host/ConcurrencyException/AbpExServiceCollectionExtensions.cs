using Abp.Modules;
using Abp;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using FMFramework.AbpEX;
using Abp.AspNetCore;

namespace FMFranework.AbpEx
{
    public static class FMFraneworkAbpExServiceCollectionExtensions
    {
        //
        // 摘要:
        //     将 GCTAbpEx 集成到 Asp.Net Core
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
        public static IServiceProvider AddGCTAbpEx<TStartupModule>(this IServiceCollection services, Action<FMFraneworkAbpExBootstrapperOptions> optionsAction = null, bool removeConventionalInterceptors = true) where TStartupModule : AbpModule
        {
            FMFraneworkAbpExBootstrapperOptions gCTAbpExBootstrapperOptions = new FMFraneworkAbpExBootstrapperOptions();
            optionsAction?.Invoke(gCTAbpExBootstrapperOptions);
            services.AddAbpWithoutCreatingServiceProvider<TStartupModule>(gCTAbpExBootstrapperOptions.AbpOptionsAction, removeConventionalInterceptors);
            AbpBootstrapper singletonServiceOrNull = services.GetSingletonServiceOrNull<AbpBootstrapper>();
            gCTAbpExBootstrapperOptions?.AbpConfigureAspNetCoreAfter?.Invoke(services);
            return WindsorRegistrationHelper.CreateServiceProvider(singletonServiceOrNull.IocManager.IocContainer, services);
        }
    }
}