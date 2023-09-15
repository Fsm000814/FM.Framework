using System.Linq;
using System.Threading.Tasks;

using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Services;

namespace FM.FrameWork.Ndo.DomainService
{
    /// <summary>
    /// Ndo 业务特有的 DomainService
    /// </summary>
    /// <typeparam name="TEntity">类型</typeparam>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public interface INdoDomainService<TEntity, TPrimaryKey> : IDomainService, ITransientDependency where TEntity : class, IEntity<TPrimaryKey>
    {
        /// <summary>
        /// TEntity 仓储
        /// </summary>
        IRepository<TEntity, TPrimaryKey> Repo { get; }

        /// <summary>
        /// TEntity 查询器
        /// </summary>
        IQueryable<TEntity> Query { get; }

        /// <summary>
        ///  TEntity 查询器 - 不追踪
        /// </summary>
        IQueryable<TEntity> QueryAsNoTracking { get; }

        /// <summary>
        /// 获取 TEntity 并包含导航属性的查询器
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetNdoIncludeQuery();

        /// <summary>
        /// 根据id查找 并包含导航属性
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> FindNdoByIdWithInclude(TPrimaryKey id);

        /// <summary>
        /// 获取一个 对象的新 Id
        /// </summary>
        /// <returns></returns>
        TPrimaryKey NewNdoId();

        /// <summary>
        /// 根据id查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> FindNdoById(TPrimaryKey id);

        /// <summary>
        /// 检查 是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> IsExistNdo(TPrimaryKey id);

        /// <summary>
        /// 直接将 对象新增到数据库，经过校验方法
        /// </summary>
        /// <param name="ndo"></param>
        /// <returns></returns>
        Task<TEntity> CreateNdo(TEntity ndo);

        /// <summary>
        /// 直接将 对象更新到数据库，不经过校验方法
        /// </summary>
        /// <param name="ndo"></param>
        /// <returns></returns>
        Task<TEntity> UpdateNdo(TEntity ndo);

        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteNdo(TPrimaryKey id);

        /// <summary>
        /// 根据对象删除
        /// </summary>
        /// <param name="ndo"></param>
        /// <returns></returns>
        Task DeleteNdo(TEntity ndo);

        /// <summary>
        /// 判断Ndo是否可以删除
        /// </summary>
        /// <param name="ndo"></param>
        /// <returns></returns>
        Task ValidateNdoOnDelete(TEntity ndo);

        /// <summary>
        /// 校验Ndo数据正确性 - 新增和修改
        /// </summary>
        /// <param name="ndo"></param>
        /// <returns></returns>
        Task ValidateNdoOnCreateOrUpdate(TEntity ndo);
    }
}
