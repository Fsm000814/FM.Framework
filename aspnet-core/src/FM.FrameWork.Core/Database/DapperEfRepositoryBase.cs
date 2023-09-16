using System;
using System.Data.Common;
using System.Threading.Tasks;

using Abp.Data;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;

using FM.FrameWork.Repository;

namespace FM.FrameWork.Database
{
    public class DapperEfRepositoryBase<TDbContext, TEntity> : DapperEfRepositoryBase<TDbContext, TEntity, int>, IDapperRepository<TEntity>, IDapperRepository<TEntity, int>, IRepository, ITransientDependency where TDbContext : class where TEntity : class, IEntity<int>
    {
        public DapperEfRepositoryBase(IActiveTransactionProvider activeTransactionProvider, ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
            : base(activeTransactionProvider, currentUnitOfWorkProvider)
        {
        }
    }
    
}
