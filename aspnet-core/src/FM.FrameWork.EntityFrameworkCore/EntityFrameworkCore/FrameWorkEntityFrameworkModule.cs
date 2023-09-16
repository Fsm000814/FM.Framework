using Abp.Dependency;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;

using Castle.MicroKernel.Registration;

using FM.FrameWork.Configuration;
using FM.FrameWork.Database;
using FM.FrameWork.EntityFrameworkCore.Extenstions;
using FM.FrameWork.EntityFrameworkCore.Seed;

namespace FM.FrameWork.EntityFrameworkCore
{
    [DependsOn(
        typeof(FrameWorkCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class FrameWorkEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!FMFrameWorkConfigs.Database.SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<FrameWorkDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        FrameWorkDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        FrameWorkDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }

            if (DatabaseInfo.Instance.DatabaseType == DatabaseTypeEnum.Oracle)
            {
                Configuration.UnitOfWork.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            }

            // 启动实体历史变更记录功能
            //Configuration.EntityHistory.IsEnabled = true;

            // 取消下面一行的注释，获取启动了实体变更历史记录的列表:
            //Configuration.EntityHistory.Selectors.Add("52ABP_Pro", EntityHistoryHelper.TrackedTypes);
            //Configuration.CustomConfigProviders.Add(new EntityHistoryConfigProvider(Configuration));
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FrameWorkEntityFrameworkModule).GetAssembly());

            IocManager.RegisterAssemblyByConvention(this.GetType().GetAssembly());
            IocManager.Register(
                typeof(IDatabaseChecker),
                typeof(DatabaseChecker<FrameWorkDbContext>),
                DependencyLifeStyle.Transient
            );

            if (IocManager.IsRegistered<ISqlExecutor>())
            {
                IocManager.IocContainer.Register(
                    Component.For<ISqlExecutor>()
                        .UsingFactoryMethod(kernel =>
                        {
                            return kernel.Resolve<FMFrameWorkSqlExecutor>();
                        })
                        .LifestyleTransient()
                        .IsDefault()
                );
            }
        }

        public override void PostInitialize()
        {
            //var configurationAccessor = IocManager.Resolve<IAppConfigurationAccessor>();
            //using var scope = IocManager.CreateScope();
            //var connStr = configurationAccessor.Configuration.ConnectionStringsDefault();
            //var dbExist = scope.Resolve<IDatabaseChecker<FrameWorkDbContext>>()
            //    .Exist(connStr);
            var dbExist = true;
            if (!FMFrameWorkConfigs.Database.SkipDbSeed && dbExist)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
