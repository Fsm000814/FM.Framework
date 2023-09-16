using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Timing;

using FM.FrameWork.Entities.Auditing;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Extenions
{
    public static class EmptyDefaultData
    {
        public static void Empty(FullAuditedEntity<Guid> entity)
        {
            if (entity != null)
            {
                entity.CreationTime = Clock.Now;
                entity.LastModificationTime = null;
                entity.LastModifierUserId = null;
                entity.CreatorUserId = null;
            }
        }

        /// <summary>
        /// 置空对象的审计信息
        /// </summary>
        /// <param name="entity"></param>
        public static void EmptyAudit(this IEntity<Guid> entity)
        {
            entity.ResetAuditing();
        }

        /// <summary>
        /// 置空对象的审计信息
        /// </summary>
        /// <param name="entity"></param>
        public static void EmptyAudit(this IEntityDto<Guid> entity)
        {
            entity.ResetAuditing();
        }
    }
}
