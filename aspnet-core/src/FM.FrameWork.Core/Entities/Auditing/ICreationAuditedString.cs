using Abp.Domain.Entities.Auditing;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Entities.Auditing
{
    /// <summary>
    /// 此接口由想要存储创建信息的实体实现
    ///（创建人和创建时间）。创建时间和创建者用户自动设置
    /// 将Abp.Domain.Enties.Entity保存到数据库时。
    /// </summary>
    public interface ICreationAuditedString : IHasCreationTime
    {
        /// <summary>
        /// 此实体的创建者用户的Id。
        /// </summary>
        string CreatorUserId { get; set; }
    }
}
