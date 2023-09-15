using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

using System;

namespace FM.FrameWork.Entities.Auditing
{
    /// <summary>
    /// 此类可用于简化Abp.Domain.Enties.Auditing.ICreationAudited的实现。
    /// </summary>
    /// <typeparam name="TPrimaryKey">实体主键的类型</typeparam>
    [Serializable]
    public abstract class CreationAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, ICreationAudited, IHasCreationTime
    {
        /// <summary>
        /// 此实体的创建时间。
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 该实体的创建者。
        /// </summary>
        public virtual string CreatorUserId { get; set; }

        /// <summary>
        /// 默认赋值创建时间为当前时间
        /// </summary>
        protected CreationAuditedEntity()
        {
            CreationTime = Clock.Now;
        }
    }
}
