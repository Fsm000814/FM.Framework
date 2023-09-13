using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System;
using FM.Framework.Core;
using FM.Framework.Web.Core.Common;
using FM.Framework.Core.App;
using FM.Framework.Core.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using FM.Framework.Web.Core;
using FM.Framework.Startup;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Castle.Services.Logging.SerilogIntegration;
using Microsoft.AspNetCore.Mvc.FMFramework;

namespace FM.Framework.Web.Host.Startup
{
    public class Startup
    {
        private const string DefaultCorsPolicyName = "FMFramework";

        private readonly IWebHostEnvironment _env;
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            _env = env;
            Configuration = env.GetAppConfiguration();
            InitWebConsts();
        }

        /// <summary>
        /// 配置一些中间件
        /// </summary>
        private void InitWebConsts()
        {
            WebConsts.HangfireDashboardEnabled = Configuration.GetValue<bool>("App:HangfireDashboardEnabled");
            WebConsts.MinioConfig.IsEnabled = Configuration.GetValue<bool>("App:FileManagement:Minio:IsEnabled");
            WebConsts.MinioConfig.AccessKey = Configuration["App:FileManagement:Minio:AccessKey"];
            WebConsts.MinioConfig.SecretKey = Configuration["App:FileManagement:Minio:SecretKey"];
            WebConsts.MinioConfig.Endpoint = Configuration["App:FileManagement:Minio:Endpoint"];
            WebConsts.MinioConfig.BucketName = Configuration["App:FileManagement:Minio:BucketName"];
            //读取Minio配置
            Console.WriteLine($"MedProWebConsts.MinioConfig{WebConsts.MinioConfig.IsEnabled},{WebConsts.MinioConfig.Endpoint}");
            WebConsts.RedisDatabaseId = Configuration.GetValue<int>("Cache:Redis:DatabaseId");
            WebConsts.RedisConnectionStr = Configuration["Cache:Redis:ConnectionString"];
            WebConsts.SignalRDatabaseId = Configuration.GetValue<int>("Cache:Redis:SignalRDatabaseId");
            WebConsts.ConnectionSignalRString = Configuration["Cache:Redis:ConnectionSignalRString"];
            AppVersionHelper.Version = Configuration["App:Version"];
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // 跳过DbContext注册
            Configs.Database.SkipDbContextRegistration = true;
            // 跳过种子数据
            Configs.Database.SkipDbSeed = true;
            // 跳过权限注册
            Configs.Authorization.SkipAuthorization = true;
            // 加载数据库配置
            DatabaseInfo.LoadConfiguraion(Configuration);

            return services.AddFMFrameworkAbp<FMFrameworkWebHostModule>(options =>
            {
                options.AbpOptionsAction = (abpOptions) =>
                {
                    abpOptions.IocManager.IocContainer.AddFacility<LoggingFacility>(f =>
                    {
                        f.LogUsing(new SerilogFactory(Log.Logger));
                    });
                };

                options.AbpConfigureAspNetCoreAfter = (s) =>
                {
                    s.Configure<MvcOptions>(mvcOptions =>
                    {
                        mvcOptions.AddTrackExceptionFilter();
                    });
                };
            }
           );
        }

        public void Configure(IApplicationBuilder app)
        {
            //// 基本的中间件
            //UseBasic(app);

            //// Hangfire
            //UseHangfire(app);

            //app.UseModelESignCheckMiddleware();

            //// EndPoint
            //app.UseEndpoints(endpoints =>
            //{
            //    // signalr
            //    endpoints.MapHub<AbpCommonHub>("/signalr");
            //    endpoints.MapHub<ChatHub>("/signalr-chat");
            //    endpoints.MapHub<PrintHub>("/signalr-print");

            //    // mvc/webapi
            //    endpoints.MapDefaultControllerRoute();
            //    endpoints.MapRazorPages();

            //    // 健康检查
            //    if (Configuration.HealthChecksEnabled())
            //    {
            //        endpoints.MapHealthChecks("/healthz",
            //            new HealthCheckOptions()
            //            {
            //                Predicate = healthCheckRegistration => true,
            //                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            //            });
            //        endpoints.MapHealthChecksUI();
            //    }
            //});

            //// 注册swagger
            //UseSwaggerUI(app);
        }

    }
}
