using Abp.Dependency;
using Abp.PlugIns;
using Abp;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FM.FrameWork.Web.Host.Startup
{
    /// <summary>
    /// Abp引导程序选项
    /// </summary>
    public class FMFrameWorkAbpBootstrapperOptions
    {
        /// <summary>
        /// abp启动配置选项
        /// </summary>
        public Action<AbpBootstrapperOptions> AbpOptionsAction { get; set; }

        /// <summary>
        /// abp配置asp.net core服务之后
        /// </summary>
        public Action<IServiceCollection> AbpConfigureAspNetCoreAfter { get; set; }
    }
}
