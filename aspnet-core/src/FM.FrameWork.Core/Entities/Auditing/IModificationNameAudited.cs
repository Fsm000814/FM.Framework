using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Entities.Auditing
{
    /// <summary>
    /// 修改用户名审计接口
    /// </summary>
    public interface IModificationNameAudited
    {
        /// <summary>
        /// 修改用户
        /// </summary>
        string LastModifierUserName { get; set; }
    }
}
