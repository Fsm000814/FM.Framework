﻿using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

using FM.FrameWork.Authorization;
using FM.FrameWork.UomModule.UOMService.Mapper;

namespace FM.FrameWork
{
    [DependsOn(
        typeof(FrameWorkCoreModule),
        typeof(AbpAutoMapperModule))]
    public class FrameWorkApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<FrameWorkAuthorizationProvider>();
            ConfigureAutoMapper();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(FrameWorkApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
            Configuration.Auditing.IsEnabled = false;
        }
        private void ConfigureAutoMapper()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(configuration =>
            {
                UOMDtoAutoMapper.CreateMappings(configuration);
            });
        }
    }
}
