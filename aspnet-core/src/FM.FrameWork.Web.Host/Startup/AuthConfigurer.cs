using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Abp.Runtime.Security;

namespace FM.FrameWork.Web.Host.Startup
{
    /// <summary>
    /// 身份验证配置程序
    /// </summary>
    public static class AuthConfigurer
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            if (bool.Parse(configuration["Authentication:JwtBearer:IsEnabled"]))
            {
                services.AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = "JwtBearer";
                    options.DefaultChallengeScheme = "JwtBearer";
                }).AddJwtBearer("JwtBearer", options =>
                {
                    options.Audience = configuration["Authentication:JwtBearer:Audience"];

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // 签名密钥必须匹配！
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:JwtBearer:SecurityKey"])),

                        // 验证JWT发行人（ISS）声明
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Authentication:JwtBearer:Issuer"],

                        // 验证JWT（AUD）
                        ValidateAudience = true,
                        ValidAudience = configuration["Authentication:JwtBearer:Audience"],

                        // 验证令牌过期
                        ValidateLifetime = true,

                        // 如果你想允许一定量的时间偏移，在这里设置
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = QueryStringTokenResolver
                    };
                });
            }
        }

        /// <summary>
        /// 此方法是授权SignalR javascript客户端所必需的。SignalR无法发送授权标头。所以，我们从查询字符串中获得它作为加密文本。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static Task QueryStringTokenResolver(MessageReceivedContext context)
        {
            if (!context.HttpContext.Request.Path.HasValue ||
                !context.HttpContext.Request.Path.Value.StartsWith("/signalr"))
            {
                // We are just looking for signalr clients
                //我们只是在找signalr的客户端
                return Task.CompletedTask;
            }

            var qsAuthToken = context.HttpContext.Request.Query["enc_auth_token"].FirstOrDefault();
            if (qsAuthToken == null)
            {
                // Cookie值与查询字符串值不匹配
                return Task.CompletedTask;
            }

            // 从cookie设置验证令牌
            context.Token = SimpleStringCipher.Instance.Decrypt(qsAuthToken);
            return Task.CompletedTask;
        }
    }
}
