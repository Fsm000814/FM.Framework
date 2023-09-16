using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FM.FrameWork.Entities.Auditing
{
    [Serializable]
    public abstract class FullAuditedEntity<TPrimaryKey> : CreationAuditedEntity<TPrimaryKey>, IFullAudited, IAudited, ICreationAudited, IHasCreationTime, IModificationAudited, IHasModificationTime, IDeletionAudited, IHasDeletionTime, ISoftDelete
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 删除人
        /// </summary>
        public virtual long? DeleterUserId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public virtual long? LastModifierUserId { get; set; }

        /// <summary>
        /// 删除人名称
        /// </summary>
        [MaxLength(32)]
        public virtual string DeleterUserName { get; set; }

        /// <summary>
        /// 修改人名称
        /// </summary>
        [MaxLength(32)]
        public virtual string LastModifierUserName { get; set; }
    }
}
