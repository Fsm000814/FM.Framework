using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using FM.FrameWork.Configuration;

namespace FM.FrameWork.Web.Host.Startup
{
    /// <summary>
    /// FrameWork.Web.Host 模块配置
    /// </summary>
    [DependsOn(
       typeof(FrameWorkWebCoreModule))]
    public class FrameWorkWebHostModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        /// <summary>
        /// FrameWork.Web.Host 模块配置
        /// </summary>
        /// <param name="env"></param>
        public FrameWorkWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FrameWorkWebHostModule).GetAssembly());
        }

        /// <summary>
        /// 预先初始化
        /// </summary>
        public override void PreInitialize()
        {
            //配置审计日志开关
            Configuration.Auditing.IsEnabled = true; 
        }
    }
}
