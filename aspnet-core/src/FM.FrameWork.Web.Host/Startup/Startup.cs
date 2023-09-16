using Abp.AspNetCore;
using Abp.AspNetCore.Mvc.Antiforgery;
using Abp.AspNetCore.SignalR.Hubs;
using Abp.Castle.Logging.Log4Net;
using Abp.Dependency;
using Abp.Extensions;
using Abp.Json;

using Castle.Facilities.Logging;
using Castle.Services.Logging.SerilogIntegration;
using Serilog;
using FM.FrameWork.Configuration;
using FM.FrameWork.Identity;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json.Serialization;

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace FM.FrameWork.Web.Host.Startup
{
    public class Startup
    {
        private const string _defaultCorsPolicyName = "localhost";

        private const string _apiVersion = "v1";

        private IConfigurationRoot _appConfiguration;
        private readonly IWebHostEnvironment _env;

        public Startup(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
            InitWebConsts();
        }

        /// <summary>
        /// 配置一些中间件，参照ZD的提交记录
        /// </summary>
        private void InitWebConsts()
        {
            //WebConsts.HangfireDashboardEnabled = Configuration.GetValue<bool>("App:HangfireDashboardEnabled");

            //MedProWebConsts.MinioConfig.IsEnabled = Configuration.GetValue<bool>("App:FileManagement:Minio:IsEnabled");
            //MedProWebConsts.MinioConfig.AccessKey = Configuration["App:FileManagement:Minio:AccessKey"];
            //MedProWebConsts.MinioConfig.SecretKey = Configuration["App:FileManagement:Minio:SecretKey"];
            //MedProWebConsts.MinioConfig.Endpoint = Configuration["App:FileManagement:Minio:Endpoint"];
            //MedProWebConsts.MinioConfig.BucketName = Configuration["App:FileManagement:Minio:BucketName"];
            //Console.WriteLine(
            //    $"MedProWebConsts.MinioConfig{MedProWebConsts.MinioConfig.IsEnabled},{MedProWebConsts.MinioConfig.Endpoint}");
            //MedProWebConsts.RedisDatabaseId = Configuration.GetValue<int>("Cache:Redis:DatabaseId");
            //MedProWebConsts.RedisConnectionStr = Configuration["Cache:Redis:ConnectionString"];
            //MedProWebConsts.SignalRDatabaseId = Configuration.GetValue<int>("Cache:Redis:SignalRDatabaseId");
            //MedProWebConsts.ConnectionSignalRString = Configuration["Cache:Redis:ConnectionSignalRString"];
            //AppVersionHelper.Version = Configuration["App:Version"];
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //MVC
            services.AddControllersWithViews(
                options => { options.Filters.Add(new AbpAutoValidateAntiforgeryTokenAttribute()); }
            ).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new AbpMvcContractResolver(IocManager.Instance)
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            });

            IdentityRegistrar.Register(services);
            AuthConfigurer.Configure(services, _appConfiguration);

            services.AddSignalR();

            // 为UI配置CORS
            services.AddCors(
                options => options.AddPolicy(
                    _defaultCorsPolicyName,
                    builder => builder
                        .WithOrigins(
                            // 应用程序：json中的CorsOrigins可以包含多个以逗号分隔的地址。
                            _appConfiguration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                )
            );

            // Swagger-启用此行和Configure方法中的相关行以启用swagger UI
            ConfigureSwagger(services);

            //配置ABP和依赖项注入
            //services.AddAbpWithoutCreatingServiceProvider<FrameWorkWebHostModule>(
            //    // 配置日志4 Net日志记录
            //    //options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
            //    //     //f => f.UseAbpLog4Net().WithConfig(_hostingEnvironment.IsDevelopment()
            //    //     //    ? "log4net.config"
            //    //     //    : "log4net.Production.config"
            //    //     //)
            //    //     f =>
            //    //     {
            //    //         f.LogUsing(new SerilogFactory(Log.Logger));
            //    //     }
            //);
            return services.AddFMFrameWorkAbp<FrameWorkWebHostModule>(options =>
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
            app.UseAbp(options => { options.UseAbpRequestLocalization = false; }); // 初始化ABP框架。

            app.UseCors(_defaultCorsPolicyName); //启用CORS！

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAbpRequestLocalization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<AbpCommonHub>("/signalr");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
            });

            // 使中间件能够将生成的Swagger作为JSON端点提供服务
            app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });

            // 使中间件能够提供swagger-ui资产(HTML、JS、CSS等)
            app.UseSwaggerUI(options =>
            {
                // 指定Swagger JSON端点。
                options.SwaggerEndpoint($"/swagger/{_apiVersion}/swagger.json", $"FrameWork API {_apiVersion}");
                options.IndexStream = () => Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("FM.FrameWork.Web.Host.wwwroot.swagger.ui.index.html");
                options.DisplayRequestDuration(); // 控制“Try it out”请求的请求持续时间（毫秒）的显示。
            }); // URL: /swagger
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(_apiVersion, new OpenApiInfo
                {
                    Version = _apiVersion,
                    Title = "FrameWork API",
                    Description = "FrameWork",
                    //TermsOfService = new Uri("https://example.com/terms"),

                    Contact = new OpenApiContact
                    {
                        Name = "FrameWork",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/aspboilerplate"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://github.com/aspnetboilerplate/aspnetboilerplate/blob/dev/LICENSE"),
                    }
                });
                options.DocInclusionPredicate((docName, description) => true);

                // 定义正在使用的BearerAuth方案
                options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme()
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                //向swagger添加摘要
                bool canShowSummaries = _appConfiguration.GetValue<bool>("Swagger:ShowSummaries");
                if (canShowSummaries)
                {
                    var hostXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var hostXmlPath = Path.Combine(AppContext.BaseDirectory, hostXmlFile);
                    options.IncludeXmlComments(hostXmlPath);

                    var applicationXml = $"FM.FrameWork.Application.xml";
                    var applicationXmlPath = Path.Combine(AppContext.BaseDirectory, applicationXml);
                    options.IncludeXmlComments(applicationXmlPath);

                    var webCoreXmlFile = $"FM.FrameWork.Web.Core.xml";
                    var webCoreXmlPath = Path.Combine(AppContext.BaseDirectory, webCoreXmlFile);
                    options.IncludeXmlComments(webCoreXmlPath);
                }
            });
        }
    }
}
