using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Entities.Auditing
{
    /// <summary>
    ///  删除用户名审计接口
    /// </summary>
    public interface IDeletionNameAudited
    {
        /// <summary>
        /// 删除用户名
        /// </summary>
        string DeleterUserName { get; set; }
    }
}
