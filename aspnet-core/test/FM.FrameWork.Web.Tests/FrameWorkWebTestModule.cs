using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using FM.FrameWork.EntityFrameworkCore;
using FM.FrameWork.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace FM.FrameWork.Web.Tests
{
    [DependsOn(
        typeof(FrameWorkWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class FrameWorkWebTestModule : AbpModule
    {
        public FrameWorkWebTestModule(FrameWorkEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FrameWorkWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(FrameWorkWebMvcModule).Assembly);
        }
    }
}