using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Entities.Auditing
{
    /// <summary>
    /// 创建用户名审计接口
    /// </summary>
    public interface ICreationNameAudited
    {
        /// <summary>
        /// 创建用户名
        /// </summary>
        string CreatorUserName { get; set; }
    }
}
