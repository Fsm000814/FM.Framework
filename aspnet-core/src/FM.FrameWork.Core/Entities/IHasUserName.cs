using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Entities
{
    /// <summary>
    /// AbpSession 扩展，包含用户名
    /// </summary>
    public interface IHasUserName
    {
        /// <summary>
        /// 当前登录用户名
        /// </summary>
        string UserName { get; }
    }
}
