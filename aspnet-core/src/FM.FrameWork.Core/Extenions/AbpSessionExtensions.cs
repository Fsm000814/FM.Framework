using Abp.Runtime.Session;

using FM.FrameWork.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Extenions
{
    public static class AbpSessionExtensions
    {
        //
        // 摘要:
        //     获取session中的用户名，需要AbpSesssion实现GCT.AbpEx.Runtime.Session.IHasUserName接口
        //
        // 参数:
        //   abpSession:
        public static string GetUserName(this IAbpSession abpSession)
        {
            IHasUserName hasUserName = abpSession as IHasUserName;
            if (hasUserName != null)
            {
                return hasUserName.UserName;
            }

            return string.Empty;
        }
    }
}
