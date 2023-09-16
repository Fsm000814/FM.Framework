using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using FM.FrameWork.Entities.Auditing;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Extenions
{
    public static class EntitiesExtensions
    {
        /// <summary>
        /// 置空实体对象的审计信息
        /// </summary>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <param name="entity"></param>
        public static void ResetAuditing<TPrimaryKey>(this IEntity<TPrimaryKey> entity)
        {
            if (entity != null)
            {
                ICreationAudited creationAudited = entity as ICreationAudited;
                if (creationAudited != null)
                {
                    creationAudited.CreationTime = default(DateTime);
                    creationAudited.CreatorUserId = null;
                }

                ICreationNameAudited creationNameAudited = entity as ICreationNameAudited;
                if (creationNameAudited != null)
                {
                    creationNameAudited.CreatorUserName = null;
                }

                IModificationAudited modificationAudited = entity as IModificationAudited;
                if (modificationAudited != null)
                {
                    modificationAudited.LastModificationTime = null;
                    modificationAudited.LastModifierUserId = null;
                }

                IModificationNameAudited modificationNameAudited = entity as IModificationNameAudited;
                if (modificationNameAudited != null)
                {
                    modificationNameAudited.LastModifierUserName = null;
                }

                IDeletionAudited deletionAudited = entity as IDeletionAudited;
                if (deletionAudited != null)
                {
                    deletionAudited.DeletionTime = null;
                    deletionAudited.DeleterUserId = null;
                }

                IDeletionNameAudited deletionNameAudited = entity as IDeletionNameAudited;
                if (deletionNameAudited != null)
                {
                    deletionNameAudited.DeleterUserName = null;
                }
            }
        }

        /// <summary>
        /// 置空Dto对象的审计信息
        /// </summary>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <param name="entity"></param>
        public static void ResetAuditing<TPrimaryKey>(this IEntityDto<TPrimaryKey> entity)
        {
            if (entity != null)
            {
                ICreationAudited creationAudited = entity as ICreationAudited;
                if (creationAudited != null)
                {
                    creationAudited.CreationTime = default(DateTime);
                    creationAudited.CreatorUserId = null;
                }

                ICreationNameAudited creationNameAudited = entity as ICreationNameAudited;
                if (creationNameAudited != null)
                {
                    creationNameAudited.CreatorUserName = null;
                }

                IModificationAudited modificationAudited = entity as IModificationAudited;
                if (modificationAudited != null)
                {
                    modificationAudited.LastModificationTime = null;
                    modificationAudited.LastModifierUserId = null;
                }

                IModificationNameAudited modificationNameAudited = entity as IModificationNameAudited;
                if (modificationNameAudited != null)
                {
                    modificationNameAudited.LastModifierUserName = null;
                }

                IDeletionAudited deletionAudited = entity as IDeletionAudited;
                if (deletionAudited != null)
                {
                    deletionAudited.DeletionTime = null;
                    deletionAudited.DeleterUserId = null;
                }

                IDeletionNameAudited deletionNameAudited = entity as IDeletionNameAudited;
                if (deletionNameAudited != null)
                {
                    deletionNameAudited.DeleterUserName = null;
                }
            }
        }
    }
}
