using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Abp.Zero.EntityFrameworkCore;

using FM.FrameWork.Entities;
using FM.FrameWork.Entities.Auditing;
using FM.FrameWork.Extenions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FM.FrameWork.EntityFrameworkCore
{
    public abstract class FrameWorkDbContextBase<TTenant, TRole, TUser, TSelf> : AbpZeroDbContext<TTenant, TRole, TUser, TSelf> where TTenant : AbpTenant<TUser> where TRole : AbpRole<TUser> where TUser : AbpUser<TUser> where TSelf : FrameWorkDbContextBase<TTenant, TRole, TUser, TSelf>
    {
        protected FrameWorkDbContextBase(DbContextOptions<TSelf> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            PreModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);
            ModelCreating(modelBuilder);
            PostModelCreating(modelBuilder);
        }

        //   modelBuilder:
        /// <summary>
        /// 模型创建之前
        /// </summary>
        /// <param name="modelBuilder"></param>
        public virtual void PreModelCreating(ModelBuilder modelBuilder)
        {
        }

        /// <summary>
        /// 模型配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        public abstract void ModelCreating(ModelBuilder modelBuilder);

        /// <summary>
        /// 模型创建之后
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected virtual void PostModelCreating(ModelBuilder modelBuilder)
        {
            ConfigConcurrency(modelBuilder);
        }

        /// <summary>
        /// 配置所有实现IConcurrency实体的并发列
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void ConfigConcurrency(ModelBuilder builder)
        {
            //它遍历了所有的实体类型，然后判断该实体类型是否实现了IConcurrency接口。
            foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
            {
                //如果实现了，则对该实体类型进行配置，将"ConcurrencyToken"属性设置为最大长度为32，并将其标记为并发性标记。
                if (entityType.ClrType.HasInterface<IConcurrency>())
                {
                    builder.Entity(entityType.ClrType).Property("ConcurrencyToken").HasMaxLength(32)
                        .IsConcurrencyToken();
                }
            }
        }

        /// <summary>
        /// 更新并发实体的的并发字段的值
        /// </summary>
        protected virtual void UpdateConcurrencyEntitys()
        {
            foreach (EntityEntry item in (from o in ChangeTracker.Entries()
                                          where (o.State == EntityState.Added || o.State == EntityState.Modified || o.State == EntityState.Deleted) && o.Entity is IConcurrency
                                          select o).ToList())
            {
                IConcurrency concurrency = item.Entity as IConcurrency;
                if (concurrency != null)
                {
                    concurrency.ConcurrencyToken = Guid.NewGuid().ToString("N");
                }
            }
        }
        /// <summary>
        /// 重写添加
        /// </summary>
        /// <param name="entityAsObj"></param>
        /// <param name="userId"></param>
        protected override void SetCreationAuditProperties(object entityAsObj, long? userId)
        {
            userId = base.AbpSession.UserId;
            base.SetCreationAuditProperties(entityAsObj, userId);
            ICreationNameAudited creationNameAudited = entityAsObj as ICreationNameAudited;
            if (creationNameAudited != null)
            {
                creationNameAudited.CreatorUserName = base.AbpSession.GetUserName();
            }
        }
        /// <summary>
        /// 重写修改
        /// </summary>
        /// <param name="entityAsObj"></param>
        /// <param name="userId"></param>
        protected override void SetModificationAuditProperties(object entityAsObj, long? userId)
        {
            userId = base.AbpSession.UserId;
            base.SetModificationAuditProperties(entityAsObj, userId);
            IModificationNameAudited modificationNameAudited = entityAsObj as IModificationNameAudited;
            if (modificationNameAudited != null)
            {
                modificationNameAudited.LastModifierUserName = base.AbpSession.GetUserName();
            }
        }

        /// <summary>
        /// 重写删除
        /// </summary>
        /// <param name="entityAsObj"></param>
        /// <param name="userId"></param>
        protected override void SetDeletionAuditProperties(object entityAsObj, long? userId)
        {
            userId = base.AbpSession.UserId;
            base.SetDeletionAuditProperties(entityAsObj, userId);
            IDeletionNameAudited deletionNameAudited = entityAsObj as IDeletionNameAudited;
            if (deletionNameAudited != null)
            {
                deletionNameAudited.DeleterUserName = base.AbpSession.GetUserName();
            }
        }

        public override int SaveChanges()
        {
            UpdateConcurrencyEntitys();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateConcurrencyEntitys();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
    
}
