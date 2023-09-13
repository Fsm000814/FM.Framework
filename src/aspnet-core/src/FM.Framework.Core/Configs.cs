using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Framework.Core
{
    //
    // 摘要:
    //     模块配置
    public static class Configs
    {
        //
        // 摘要:
        //     EFCore配置
        public static class Database
        {
            /// <summary>
            /// 跳过DbContext注册
            /// </summary>
            public static bool SkipDbContextRegistration { get; set; }

            /// <summary>
            /// 跳过种子数据
            /// </summary>
            public static bool SkipDbSeed { get; set; }
        }

        //
        // 摘要:
        //     权限配置
        public static class Authorization
        {
            /// <summary>
            /// 跳过权限注册
            /// </summary>
            public static bool SkipAuthorization { get; set; }
        }

        //
        // 摘要:
        //     本地化配置
        public static class Localization
        {
            /// <summary>
            /// 源名称，默认值为 FMFrameWork
            /// </summary>
            public static string SourceName { get; set; } = "FMFramework";

        }

        //
        // 摘要:
        //     多租户配置
        public static class MultiTenancy
        {
            //
            // 摘要:
            //     是否启用
            public static bool IsEnabled { get; set; }
        }

        //
        // 摘要:
        //     AbpSession配置
        public static class AbpSession
        {
            //
            // 摘要:
            //     是否需要替换默认的AbpSession
            public static bool ReplaceDefaultAbpSession { get; set; } = true;

        }
    }
}
