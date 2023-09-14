using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using FM.FrameWork.Configuration;

namespace FM.FrameWork.Web.Host.Startup
{
    [DependsOn(
       typeof(FrameWorkWebCoreModule))]
    public class FrameWorkWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public FrameWorkWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FrameWorkWebHostModule).GetAssembly());
        }
    }
}
