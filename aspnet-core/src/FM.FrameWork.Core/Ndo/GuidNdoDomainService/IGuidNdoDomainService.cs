using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Services;
using Abp.Timing;

using FM.FrameWork.Ndo.DomainService;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;

using static FM.FrameWork.FMFrameWorkConfigs;

namespace FM.FrameWork.Ndo.GuidNdoDomainService
{
    public interface IGuidNdoDomainService<TEntity> : INdoDomainService<TEntity, Guid>, IDomainService, ITransientDependency where TEntity : class, IEntity<Guid>
    {
        /// <summary>
        ///  创建一个新的Guid Id
        /// </summary>
        /// <returns></returns>
        Guid NewId();
        public Task<List<TEntity>> BatchCreateNdo(List<TEntity> entities);
        public Task<List<TEntity>> UnitOfWorkBatchCreateNdo(List<TEntity> entities, TransactionScopeOption opation = TransactionScopeOption.Required);
        public Task<List<TEntity>> BatchCreateNotIdNdo(List<TEntity> entities);
        public Task<List<TEntity>> UnitOfWorkBatchCreateNotIdNdo(List<TEntity> entities, TransactionScopeOption opation = TransactionScopeOption.Required);
        public Task<List<TEntity>> BatchUpdateNdo(List<TEntity> entities);
        public Task<List<TEntity>> UnitOfWorkBatchUpdateNdo(List<TEntity> entities, TransactionScopeOption opation = TransactionScopeOption.Required);
        public Task<List<TEntity>> BatchCreateOrUpdateNdo(List<TEntity> entities);
        public Task<List<TEntity>> BatchDeleteNdo(List<Guid> ids);
        public Task DeleteByCondition(Expression<Func<TEntity, bool>> predicate);
        public Task<List<TEntity>> BatchDeleteNdo(List<TEntity> entities);
        public Task<List<TEntity>> UnitOfWorkBatchDeleteNdo(List<TEntity> entities, TransactionScopeOption opation = TransactionScopeOption.Required);
    }
}
