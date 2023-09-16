namespace FM.FrameWork.Configuration
{
    public static class AppSettingNames
    {
        public const string UiTheme = "App.UiTheme";
        public static class System
        {
            //
            // 摘要:
            //     数据库连接字符串
            public static string ConnectionStrings_Default { get; }

            //
            // 摘要:
            //     多租户是否启用
            public static string MultiTenancy_IsEnabled { get; }

            //
            // 摘要:
            //     是否启用jwt
            public static string Authentication_JwtBearer_IsEnabled { get; }

            //
            // 摘要:
            //     jwt SecurityKey
            public static string Authentication_JwtBearer_SecurityKey { get; }

            //
            // 摘要:
            //     jwt Issuer
            public static string Authentication_JwtBearer_Issuer { get; }

            //
            // 摘要:
            //     jwt Audience
            public static string Authentication_JwtBearer_Audience { get; }

            static System()
            {
                ConnectionStrings_Default = "Default";
                MultiTenancy_IsEnabled = "MultiTenancy:IsEnabled";
                Authentication_JwtBearer_IsEnabled = "Authentication:JwtBearer:IsEnabled";
                Authentication_JwtBearer_SecurityKey = "Authentication:JwtBearer:SecurityKey";
                Authentication_JwtBearer_Issuer = "Authentication:JwtBearer:Issuer";
                Authentication_JwtBearer_Audience = "Authentication:JwtBearer:Audience";
            }
        }

        //
        // 摘要:
        //     管理应用程序配置信息
        public static class HostSettings
        {
            //
            // 摘要:
            //     发票抬头
            public const string BillingLegalName = "App.HostManagement.BillingLegalName";

            //
            // 摘要:
            //     发票地址
            public const string BillingAddress = "App.HostManagement.BillingAddress";

            //
            // 摘要:
            //     启用租户注册
            public const string AllowSelfOnTenantRegistration = "App.HostManagement.AllowSelfOnTenantRegistration";

            //
            // 摘要:
            //     启用新租户默认激活
            public const string IsNewRegisteredTenantActiveByDefault = "App.HostManagement.IsNewRegisteredTenantActiveByDefault";

            //
            // 摘要:
            //     启用租户注册验证码
            public const string UseCaptchaOnTenantRegistration = "App.HostManagement.UseCaptchaOnTenantRegistration";

            //
            // 摘要:
            //     配置租户验证码类型 0:纯数字 1:纯字母 2:数字+字母 3:纯汉字
            public const string ConfigCaptchaOnTenantType = "App.HostManagement.ConfigCaptchaOnTenantType";

            //
            // 摘要:
            //     配置租户验证码长度
            public const string ConfigCaptchaOnTenantLength = "App.HostManagement.ConfigCaptchaOnTenantLength";

            //
            // 摘要:
            //     演示模式
            public const string PreviewMode = "App.HostManagement.PreviewMode";
        }

        //
        // 摘要:
        //     租户管理
        public static class TenantSettings
        {
            //
            // 摘要:
            //     发票抬头
            public const string BillingLegalName = "App.TenantManagement.BillingLegalName";

            //
            // 摘要:
            //     发票地址
            public const string BillingAddress = "App.TenantManagement.BillingAddress";

            //
            // 摘要:
            //     发票税号
            public const string BillingTaxVatNo = "App.TenantManagement.BillingTaxVatNo";

            //
            // 摘要:
            //     新租户默认版本
            public const string DefaultEdition = "App.TenantManagement.DefaultEdition";

            //
            // 摘要:
            //     订阅过期通知日计数
            public const string SubscriptionExpireNotifyDayCount = "App.TenantManagement.SubscriptionExpireNotifyDayCount";
        }

        public static class UserSettings
        {
            //
            // 摘要:
            //     用户最后一次修改密码时间
            public const string LastUpdatePasswordTime = "App.UserManagement.LastUpdatePasswordTime";
        }

        //
        // 摘要:
        //     设置范围包含系统级和租户级
        public static class SharedSettings
        {
            //
            // 摘要:
            //     系统信息
            public static class SystemInfo
            {
                //
                // 摘要:
                //     系统名称
                public const string SystemName = "App.Shared.SystemInfo.Name";

                //
                // 摘要:
                //     版本
                public const string SystemVersion = "App.Shared.SystemInfo.Version";

                //
                // 摘要:
                //     发行信息
                public const string SystemReleaseInfo = "App.Shared.SystemInfo.ReleaseInfo";
            }

            //
            // 摘要:
            //     双重认证
            public static class TwoFactorLogin
            {
                public const string IsGoogleAuthenticatorEnabled = "App.Shared.TwoFactorLogin.IsGoogleAuthenticatorEnabled";
            }

            //
            // 摘要:
            //     网站设置 Banner/Logo
            public static class SiteSettings
            {
                public const string BannerImage = "App.Site.BannerImage";

                public const string LogoImage = "App.Site.LogoImage";

                public const string Thumbnail = "App.Site.Thumbnail";
            }

            //
            // 摘要:
            //     启用短信验证
            public const string SmsVerificationEnabled = "App.Shared.SmsVerificationEnabled";

            //
            // 摘要:
            //     启用快速样式选择
            public const string IsQuickThemeSelectEnabled = "App.Shared.IsQuickThemeSelectEnabled";

            //
            // 摘要:
            //     用户注册
            public const string AllowSelfRegistrationUser = "App.Shared.AllowSelfRegistrationUser";

            //
            // 摘要:
            //     用户注册默认激活
            public const string IsNewRegisteredUserActiveByDefault = "App.Shared.IsNewRegisteredUserActiveByDefault";

            //
            // 摘要:
            //     启用登录互斥
            public const string LoginMutex = "App.Shared.LoginMutex";

            //
            // 摘要:
            //     最大登录用户数
            public const string MaxLoginUserNumber = "App.MaxLoginUserNumber";

            //
            // 摘要:
            //     是否启用强制更新密码
            public const string UpdatePasswordTimeRuleStatus = "App.UserManagement.UpdatePasswordTimeRuleStatus";

            //
            // 摘要:
            //     密码有效时长
            public const string UpdatePasswordTimeRuleTimeLast = "App.UserManagement.UpdatePasswordTimeRuleTimeLast";

            //
            // 摘要:
            //     时长单位
            public const string UpdatePasswordTimeRuleTimeUnit = "App.UserManagement.UpdatePasswordTimeRuleTimeUnit";

            //
            // 摘要:
            //     用户注册验证码
            public const string UseCaptchaOnUserRegistration = "App.Shared.UseCaptchaOnUserRegistration";

            //
            // 摘要:
            //     用户登陆验证码
            public const string UseCaptchaOnUserLogin = "App.Shared.UseCaptchaOnUserLogin";

            //
            // 摘要:
            //     忘记密码验证码
            public const string UseCaptchaOnUserForgotPassword = "App.Shared.UseCaptchaOnUserForgotPassword";

            //
            // 摘要:
            //     配置用户验证码类型 0:纯数字 1:纯字母 2:数字+字母 3:纯汉字
            public const string ConfigCaptchaOnUserType = "App.Shared.ConfigCaptchaOnUserType";

            //
            // 摘要:
            //     配置用户验证码长度
            public const string ConfigCaptchaOnUserLength = "App.Shared.ConfigCaptchaOnUserLength";

            //
            // 摘要:
            //     登录过期时间（单位/h)
            public const string LoginExpiryTime = "App.Shared.LoginExpiryTime";

            //
            // 摘要:
            //     是否返回异常详情到客户端
            public const string IsReturnExceptionInfo = "App.Shared.IsReturnExceptionInfo";

            //
            // 摘要:
            //     布局
            public const string ThemeLayout = "App.Shared.Theme.Layout";

            //
            // 摘要:
            //     网站配置 主题管理
            public const string ProjectConfig = "App.Shared.Theme.ProjectConfig";

            //
            // 摘要:
            //     门户 url
            public const string ProtalUrl = "App.Shared.ProtalUrl";

            //
            // 摘要:
            //     管理 api url
            public const string AdminApiUrl = "App.Shared.AdminApiUrl";

            //
            // 摘要:
            //     管理界面 Url
            public const string AdminUiUrl = "App.Shared.AdminUiUrl";

            //
            // 摘要:
            //     用户中心 url
            public const string UserCenterUrl = "App.Shared.UserCenterUrl";

            //
            // 摘要:
            //     BI大屏 url
            public const string BiUrl = "App.Shared.BiUrl";

            //
            // 摘要:
            //     审计日志定期删除 上次执行的操作时间
            public const string AuditLogsLastExecuteTime = "App.AuditLog.LastExecuteDeleteTime";

            //
            // 摘要:
            //     日志保留时长(单位/天)
            public const string AuditReservedSize = "App.AuditLog.ReservedSize";
        }
    }
}
