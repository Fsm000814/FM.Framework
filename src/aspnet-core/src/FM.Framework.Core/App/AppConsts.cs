using Abp.Localization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Framework.Core.App
{
    //
    // 摘要:
    //     App的常量
    public class AppConsts
    {
        public static class CacheKeys
        {
            public const string License = "License";

            public const string LicenseLatestInfo = "LicenseLatestInfo";
        }

        //
        // 摘要:
        //     Demo
        public const string Demo = "Demo";

        //
        // 摘要:
        //     本地化源名称
        public static string LocalizationSourceName = "FMFramework";

        //
        // 摘要:
        //     默认 按照创建时间倒序
        public const string DefaultSortByCreationTimeDesc = "CreationTime desc";

        //
        // 摘要:
        //     NdoDto 按照名称排序
        public const string NdoSortByName = "Name asc";

        //
        // 摘要:
        //     RdoBase 按照名称排序 建议直接医用上面的那个
        public const string RdoBaseSortByName = "Name asc";

        //
        // 摘要:
        //     超级管理员
        public const string SuperAdmin = "SuperAdmin";

        //
        // 摘要:
        //     多语言本地化
        //
        // 参数:
        //   name:
        private static string L(string name)
        {
            return new LocalizableString(name, LocalizationSourceName).ToString();
        }
    }
}
