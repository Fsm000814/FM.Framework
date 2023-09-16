using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;

using FM.FrameWork.Database;

namespace FM.FrameWork.Repository
{
    public interface IDapperRepository<TEntity> : IDapperRepository<TEntity, int>, IRepository, ITransientDependency where TEntity : class, IEntity<int>
    {
    }
}
