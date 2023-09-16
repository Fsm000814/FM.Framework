using Abp;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Timing;
using Abp.UI;

using EFCore.BulkExtensions;

using FM.FrameWork.Entities.Auditing;
using FM.FrameWork.Extenions;
using FM.FrameWork.Ndo.DomainService;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FM.FrameWork.Ndo.GuidNdoDomainService
{
    public abstract class GuidNdoDomainService<TEntity> : NdoDomainService<TEntity, Guid>, IGuidNdoDomainService<TEntity>, INdoDomainService<TEntity, Guid>, IDomainService, ITransientDependency where TEntity : FullAuditedEntity<Guid>, IEntity<Guid>,IMayHaveTenant
    {
        public virtual IGuidGenerator GuidGenerator { get; }
        public IUnitOfWorkManager unitOfWorkManager { get; }

        public GuidNdoDomainService(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            GuidGenerator = serviceProvider.GetRequiredService<IGuidGenerator>();
        }

        public override Guid NewNdoId()
        {
            return NewId();
        }

        public virtual Guid NewId()
        {
            return GuidGenerator.Create();
        }

        public async Task<List<TEntity>> BatchCreateNdo(List<TEntity> entities)
        {
            DbContext dbContext = base.Repo.GetDbContext();
            int? tenantId = AbpSession.TenantId;
            foreach (TEntity entity in entities)
            {
                entity.Id = NewNdoId();
                entity.TenantId = tenantId;
                entity.CreationTime = (entity.CreationTime == default(DateTime)) ? Clock.Now : entity.CreationTime;
                entity.CreatorUserId = entity.CreatorUserId ?? AbpSession.UserId;
                entity.CreatorUserName = entity.CreatorUserName ?? AbpSession.GetUserName();
                entity.LastModifierUserId = AbpSession.UserId;
                entity.LastModificationTime = Clock.Now;
                entity.LastModifierUserName = AbpSession.GetUserName();
            }

            await dbContext.BulkInsertAsync(entities);
            return entities;
        }

        public async Task<List<TEntity>> UnitOfWorkBatchCreateNdo(List<TEntity> entities, TransactionScopeOption opation = TransactionScopeOption.Required)
        {
            using IUnitOfWorkCompleteHandle unitOfWork = unitOfWorkManager.Begin(opation);
            _ = unitOfWorkManager.Current;
            DbContext dbContext = base.Repo.GetDbContext();
            int? tenantId = AbpSession.TenantId;
            foreach (TEntity entity in entities)
            {
                entity.Id = NewNdoId();
                entity.TenantId = tenantId;
                entity.CreationTime = ((entity.CreationTime == default(DateTime)) ? Clock.Now : entity.CreationTime);
                entity.CreatorUserId = entity.CreatorUserId ?? AbpSession.UserId;
                entity.CreatorUserName = entity.CreatorUserName ?? AbpSession.GetUserName();
                entity.LastModifierUserId = AbpSession.UserId;
                entity.LastModificationTime = Clock.Now;
                entity.LastModifierUserName = AbpSession.GetUserName();
            }

            await dbContext.BulkInsertAsync(entities);
            await unitOfWork.CompleteAsync();
            return entities;
        }

        public async Task<List<TEntity>> BatchCreateNotIdNdo(List<TEntity> entities)
        {
            DbContext dbContext = base.Repo.GetDbContext();
            int? tenantId = AbpSession.TenantId;
            foreach (TEntity entity in entities)
            {
                entity.TenantId = tenantId;
                entity.CreationTime = ((entity.CreationTime == default(DateTime)) ? Clock.Now : entity.CreationTime);
                entity.CreatorUserId = entity.CreatorUserId ?? AbpSession.UserId;
                entity.CreatorUserName = entity.CreatorUserName ?? AbpSession.GetUserName();
                entity.LastModifierUserId = AbpSession.UserId;
                entity.LastModificationTime = Clock.Now;
                entity.LastModifierUserName = AbpSession.GetUserName();
            }

            await dbContext.BulkInsertAsync(entities);
            return entities;
        }

        public async Task<List<TEntity>> UnitOfWorkBatchCreateNotIdNdo(List<TEntity> entities, TransactionScopeOption opation = TransactionScopeOption.Required)
        {
            using IUnitOfWorkCompleteHandle unitOfWork = unitOfWorkManager.Begin(opation);
            _ = unitOfWorkManager.Current;
            DbContext dbContext = base.Repo.GetDbContext();
            int? tenantId = AbpSession.TenantId;
            foreach (TEntity entity in entities)
            {
                entity.TenantId = tenantId;
                entity.CreationTime = ((entity.CreationTime == default(DateTime)) ? Clock.Now : entity.CreationTime);
                entity.CreatorUserId = entity.CreatorUserId ?? AbpSession.UserId;
                entity.CreatorUserName = entity.CreatorUserName ?? AbpSession.GetUserName();
                entity.LastModifierUserId = AbpSession.UserId;
                entity.LastModificationTime = Clock.Now;
                entity.LastModifierUserName = AbpSession.GetUserName();
            }

            await dbContext.BulkInsertAsync(entities);
            await unitOfWork.CompleteAsync();
            return entities;
        }

        public async Task<List<TEntity>> BatchUpdateNdo(List<TEntity> entities)
        {
            DbContext dbContext = base.Repo.GetDbContext();
            foreach (TEntity entity in entities)
            {
                entity.LastModifierUserId = AbpSession.UserId;
                entity.LastModificationTime = Clock.Now;
                entity.LastModifierUserName = AbpSession.GetUserName();
            }

            await dbContext.BulkUpdateAsync(entities);
            return entities;
        }

        public async Task<List<TEntity>> UnitOfWorkBatchUpdateNdo(List<TEntity> entities, TransactionScopeOption opation = TransactionScopeOption.Required)
        {
            using IUnitOfWorkCompleteHandle unitOfWork = unitOfWorkManager.Begin(opation);
            DbContext dbContext = base.Repo.GetDbContext();
            foreach (TEntity entity in entities)
            {
                entity.LastModifierUserId = AbpSession.UserId;
                entity.LastModificationTime = Clock.Now;
                entity.LastModifierUserName = AbpSession.GetUserName();
            }

            await dbContext.BulkUpdateAsync(entities);
            await unitOfWork.CompleteAsync();
            return entities;
        }

        public async Task<List<TEntity>> BatchCreateOrUpdateNdo(List<TEntity> entities)
        {
            DbContext dbContext = base.Repo.GetDbContext();
            int? tenantId = AbpSession.TenantId;
            foreach (TEntity entity in entities)
            {
                if (entity.Id == Guid.Empty)
                {
                    entity.Id = NewNdoId();
                    entity.TenantId = tenantId;
                    entity.CreationTime = ((entity.CreationTime == default(DateTime)) ? Clock.Now : entity.CreationTime);
                    entity.CreatorUserId = entity.CreatorUserId ?? AbpSession.UserId;
                    entity.CreatorUserName = entity.CreatorUserName ?? AbpSession.GetUserName();
                }

                entity.LastModifierUserId = AbpSession.UserId;
                entity.LastModificationTime = Clock.Now;
                entity.LastModifierUserName = AbpSession.GetUserName();
            }

            await dbContext.BulkInsertOrUpdateAsync(entities);
            return entities;
        }

        public async Task<List<TEntity>> BatchDeleteNdo(List<Guid> ids)
        {
            List<TEntity> entityList = await base.Query.Where((TEntity item) => ids.Contains(item.Id)).ToListAsync();
            foreach (TEntity item2 in entityList)
            {
                await ValidateNdoOnDelete(item2);
                item2.IsDeleted = true;
                item2.DeleterUserId = AbpSession.UserId;
                item2.DeletionTime = Clock.Now;
                item2.LastModifierUserId = AbpSession.UserId;
                item2.LastModificationTime = Clock.Now;
            }

            await base.Repo.GetDbContext().BulkUpdateAsync(entityList);
            return entityList;
        }

        public async Task DeleteByCondition(Expression<Func<TEntity, bool>> predicate)
        {
            await base.Repo.DeleteAsync(predicate);
        }

        protected virtual void NullError(string str = "NullError")
        {
            throw new UserFriendlyException(L("Error"), L(str, Clock.Now.ToString("yyyy-MM-dd HH:mm:ss")));
        }

        public async Task<List<TEntity>> BatchDeleteNdo(List<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                entity.IsDeleted = true;
                entity.DeleterUserId = AbpSession.UserId;
                entity.DeletionTime = Clock.Now;
                entity.LastModifierUserId = AbpSession.UserId;
                entity.LastModificationTime = Clock.Now;
            }

            await base.Repo.GetDbContext().BulkUpdateAsync(entities);
            return entities;
        }

        public async Task<List<TEntity>> UnitOfWorkBatchDeleteNdo(List<TEntity> entities, TransactionScopeOption opation = TransactionScopeOption.Required)
        {
            using IUnitOfWorkCompleteHandle unitOfWork = unitOfWorkManager.Begin(opation);
            foreach (TEntity entity in entities)
            {
                entity.IsDeleted = true;
                entity.DeleterUserId = AbpSession.UserId;
                entity.DeletionTime = Clock.Now;
                entity.LastModifierUserId = AbpSession.UserId;
                entity.LastModificationTime = Clock.Now;
            }

            await base.Repo.GetDbContext().BulkUpdateAsync(entities);
            await unitOfWork.CompleteAsync();
            return entities;
        }
    }
}
