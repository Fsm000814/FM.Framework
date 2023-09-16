using Abp.Modules;
using Abp;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using Abp.AspNetCore;

namespace FM.FrameWork.Web.Host.Startup
{
    public static class FMFrameWorkAbpExServiceCollectionExtensions
    {
        public static IServiceProvider AddFMFrameWorkAbpEx<TStartupModule>(this IServiceCollection services, Action<FMFrameWorkAbpBootstrapperOptions> optionsAction = null, bool removeConventionalInterceptors = true) where TStartupModule : AbpModule
        {
            FMFrameWorkAbpBootstrapperOptions frameWorkAbpBootstrapperOptions = new FMFrameWorkAbpBootstrapperOptions();
            optionsAction?.Invoke(frameWorkAbpBootstrapperOptions);
            services.AddAbpWithoutCreatingServiceProvider<TStartupModule>(frameWorkAbpBootstrapperOptions.AbpOptionsAction, removeConventionalInterceptors);
            AbpBootstrapper singletonServiceOrNull = services.GetSingletonServiceOrNull<AbpBootstrapper>();
            frameWorkAbpBootstrapperOptions?.AbpConfigureAspNetCoreAfter?.Invoke(services);
            return WindsorRegistrationHelper.CreateServiceProvider(singletonServiceOrNull.IocManager.IocContainer, services);
        }
    }
}
