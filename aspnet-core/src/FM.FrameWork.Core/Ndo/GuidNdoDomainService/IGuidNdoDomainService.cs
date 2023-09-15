using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Services;

using FM.FrameWork.Ndo.DomainService;

using System;

namespace FM.FrameWork.Ndo.GuidNdoDomainService
{
    public interface IGuidNdoDomainService<TEntity> : INdoDomainService<TEntity, Guid>, IDomainService, ITransientDependency where TEntity : class, IEntity<Guid>
    {
        /// <summary>
        ///  创建一个新的Guid Id
        /// </summary>
        /// <returns></returns>
        Guid NewId();
    }
}
