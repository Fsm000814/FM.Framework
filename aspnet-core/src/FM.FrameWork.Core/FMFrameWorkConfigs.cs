using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork
{
    public static class FMFrameWorkConfigs
    {
        public static class Dto
        {
            //
            // 摘要:
            //     默认 按照创建时间倒序
            public static string DefaultSortByCreationTimeDesc { get; } = "CreationTime desc";


            //
            // 摘要:
            //     NdoDto 按照名称排序
            public static string NdoSortByName { get; } = "Name asc";


            //
            // 摘要:
            //     RdoBase 按照名称排序 建议直接医用上面的那个
            public static string RdoBaseSortByName { get; } = "Name asc";

        }

        //
        // 摘要:
        //     EFCore配置
        public static class Database
        {
            //
            // 摘要:
            //     跳过DbContext注册
            public static bool SkipDbContextRegistration { get; set; }

            //
            // 摘要:
            //     跳过种子数据
            public static bool SkipDbSeed { get; set; }
        }

        //
        // 摘要:
        //     权限配置
        public static class Authorization
        {
            //
            // 摘要:
            //     跳过权限注册
            public static bool SkipAuthorization { get; set; }
        }

        //
        // 摘要:
        //     本地化配置
        public static class Localization
        {
            //
            // 摘要:
            //     源名称，默认值为 GCTMedPro
            public static string SourceName { get; set; } = "FMFrameWork";

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
