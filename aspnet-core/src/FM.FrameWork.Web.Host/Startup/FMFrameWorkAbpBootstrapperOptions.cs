using Abp.Dependency;
using Abp.PlugIns;
using Abp;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FM.FrameWork.Web.Host.Startup
{
    public class FMFrameWorkAbpBootstrapperOptions
    {
        //
        // 摘要:
        //     abp启动配置选项
        public Action<AbpBootstrapperOptions> AbpOptionsAction { get; set; }

        //
        // 摘要:
        //     abp配置asp.net core服务之后
        public Action<IServiceCollection> AbpConfigureAspNetCoreAfter { get; set; }
    }
}
