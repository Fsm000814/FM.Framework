using Abp.AspNetCore.Configuration;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using FM.Framework.Core;
using FM.Framework.Web.Core.Token;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace FM.Framework.Startup
{
    public class FMFrameworkWebHostModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IWebHostEnvironment _env;

        public FMFrameworkWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            //启用ABP框架在应用服务中使用MVC的日期时间格式。
            Configuration.Modules.AbpAspNetCore().UseMvcDateTimeFormatForAppServices = true;
            //禁用多租户
            Configuration.MultiTenancy.IsEnabled = false;
            //注册Token权限
            if (!IocManager.IsRegistered<TokenAuthConfiguration>())
            {
                IocManager.Register<TokenAuthConfiguration>();
            }
            //设置访问令牌过期时间和刷新令牌过期时间都为3分钟
            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();
            tokenAuthConfig.AccessTokenExpiration = TimeSpan.FromMinutes(3);
            tokenAuthConfig.RefreshTokenExpiration = TimeSpan.FromMinutes(3);
        }

        public override void Initialize()
        {
            //依赖注入PDA权限注册
            //IocManager.Resolve<PdaAuthoritysManager>().Initialize();
            //将当前程序集中的所有类以默认的方式注册到依赖注入容器中。这样可以实现自动注册的功能，无需手动一个个进行注册。
            IocManager.RegisterAssemblyByConvention(GetType().GetAssembly());
        }

        public override void PostInitialize()
        {
            // 生产环境关闭异常信息发送
            if (_env.IsProduction())
            {
                //在生产环境下不将详细的异常信息发送给客户端，以增加系统的安全性和防止信息泄露。
                Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = false;
            }
        }
    }
}
