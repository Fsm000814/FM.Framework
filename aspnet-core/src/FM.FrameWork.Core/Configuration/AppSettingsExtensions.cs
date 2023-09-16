using Microsoft.Extensions.Configuration;

namespace FM.FrameWork.Configuration
{
    public static class AppSettingsExtensions
    {
        //
        // 摘要:
        //     是否开启多租户
        private static bool? MultiTenancy_IsEnabled { get; set; }

        //
        // 摘要:
        //     数据库驱动类型
        private static DatabaseDrivenType? DrivenType { get; set; }

        //
        // 摘要:
        //     默认数据库连接字符串
        //
        // 参数:
        //   configuration:
        public static string ConnectionStringsDefault(this IConfiguration configuration)
        {
            return configuration.GetConnectionString(AppSettingNames.System.ConnectionStrings_Default);
        }

        //
        // 摘要:
        //     根据配置获取驱动类型
        //
        // 参数:
        //   configuration:
        public static DatabaseDrivenType GetDatabaseDrivenType(this IConfiguration configuration)
        {
            if (DrivenType.HasValue)
            {
                return DrivenType.Value;
            }

            switch (configuration["ConnectionStrings:DatabaseDrivenType"]?.ToLower())
            {
                case "postgresql":
                    DrivenType = DatabaseDrivenType.PostgreSQL;
                    break;
                case "mysql":
                    DrivenType = DatabaseDrivenType.MySql;
                    break;
                case "oracle":
                    DrivenType = DatabaseDrivenType.Oracle;
                    break;
                case "devart-oracle":
                    DrivenType = DatabaseDrivenType.DevartOracle;
                    break;
                default:
                    DrivenType = DatabaseDrivenType.SqlServer;
                    break;
            }

            return DrivenType.Value;
        }

        //
        // 摘要:
        //     根据配置获取 Devart 的 license
        //
        // 参数:
        //   configuration:
        public static string GetDevartLicense(this IConfiguration configuration)
        {
            return configuration["ConnectionStrings:DevartLicense"];
        }

        //
        // 摘要:
        //     是否开启多租户,默认为true
        //
        // 参数:
        //   configuration:
        public static bool MultiTenancyIsEnabled(this IConfiguration configuration)
        {
            if (MultiTenancy_IsEnabled.HasValue)
            {
                return MultiTenancy_IsEnabled.Value;
            }

            try
            {
                MultiTenancy_IsEnabled = configuration.GetValue<bool?>(AppSettingNames.System.MultiTenancy_IsEnabled);
            }
            catch
            {
                MultiTenancy_IsEnabled = true;
            }

            if (MultiTenancy_IsEnabled.HasValue)
            {
                return MultiTenancy_IsEnabled.Value;
            }

            return true;
        }

        //
        // 摘要:
        //     是否启用JWT,默认为true
        //
        // 参数:
        //   configuration:
        public static bool AuthenticationJwtBearerIsEnabled(this IConfiguration configuration)
        {
            bool? value = configuration.GetValue<bool?>(AppSettingNames.System.Authentication_JwtBearer_IsEnabled);
            if (value.HasValue)
            {
                return value.Value;
            }

            return true;
        }

        //
        // 摘要:
        //     jwt SecurityKey
        //
        // 参数:
        //   configuration:
        public static string AuthenticationJwtBearerSecurityKey(this IConfiguration configuration)
        {
            return configuration[AppSettingNames.System.Authentication_JwtBearer_SecurityKey];
        }

        //
        // 摘要:
        //     jwt Issuer
        //
        // 参数:
        //   configuration:
        public static string AuthenticationJwtBearerIssuer(this IConfiguration configuration)
        {
            return configuration[AppSettingNames.System.Authentication_JwtBearer_Issuer];
        }

        //
        // 摘要:
        //     jwt Audience
        //
        // 参数:
        //   configuration:
        public static string AuthenticationJwtBearerAudience(this IConfiguration configuration)
        {
            return configuration[AppSettingNames.System.Authentication_JwtBearer_Audience];
        }

        //
        // 摘要:
        //     健康检查
        //
        // 参数:
        //   configuration:
        public static bool HealthChecksEnabled(this IConfiguration configuration)
        {
            try
            {
                return configuration.GetValue<bool>("HealthChecks:HealthChecksEnabled");
            }
            catch
            {
                return false;
            }
        }

        //
        // 摘要:
        //     健康检查UI是否启用
        //
        // 参数:
        //   configuration:
        public static bool HealthChecksUIEnabled(this IConfiguration configuration)
        {
            try
            {
                return configuration.GetValue<bool>("HealthChecks:HealthChecksUI:HealthChecksUIEnabled");
            }
            catch
            {
                return false;
            }
        }

        //
        // 摘要:
        //     健康检查UI配置
        //
        // 参数:
        //   configuration:
        public static IConfigurationSection HealthChecksUI(this IConfiguration configuration)
        {
            return configuration.GetSection("HealthChecks")?.GetSection("HealthChecksUI");
        }
    }
}
