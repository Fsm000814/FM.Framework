using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Services;
using Abp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.Timing;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FM.FrameWork.Extenions;

namespace FM.FrameWork.Ndo.DomainService
{
    public abstract class NdoDomainService<TEntity, TPrimaryKey> : AbpServiceBase, INdoDomainService<TEntity, TPrimaryKey>, IDomainService, ITransientDependency where TEntity : class, IEntity<TPrimaryKey>
    {
        public virtual IServiceProvider ServiceProvider { get; private set; }

        public virtual IAbpSession AbpSession { get; private set; }

        public IRepository<TEntity, TPrimaryKey> Repo { get; }

        public IQueryable<TEntity> Query => Repo.GetAll();

        public IQueryable<TEntity> QueryAsNoTracking => Query.AsNoTracking();

        public NdoDomainService(IServiceProvider serviceProvider)
        {
            base.LocalizationSourceName = FMFrameWorkConfigs.Localization.SourceName;
            ServiceProvider = serviceProvider;
            AbpSession = serviceProvider.GetRequiredService<IAbpSession>();
            Repo = serviceProvider.GetRequiredService<IRepository<TEntity, TPrimaryKey>>();
        }

        public abstract IQueryable<TEntity> GetNdoIncludeQuery();

        public abstract TPrimaryKey NewNdoId();

        public abstract Task ValidateNdoOnDelete(TEntity ndo);

        public abstract Task ValidateNdoOnCreateOrUpdate(TEntity ndo);

        public async Task<TEntity> FindNdoByIdWithInclude(TPrimaryKey id)
        {
            return await GetNdoIncludeQuery().FirstOrDefaultAsync((TEntity x) => x.Id.Equals(id));
        }

        public async Task<TEntity> FindNdoById(TPrimaryKey id)
        {
            return await QueryAsNoTracking.FirstOrDefaultAsync((TEntity x) => x.Id.Equals(id));
        }

        public async Task<TEntity> IsExistNdo(TPrimaryKey id)
        {
            TEntity val = await FindNdoById(id);
            if (val == null)
            {
                throw new UserFriendlyException(L("Error"), L("NullError", Clock.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            }

            return val;
        }

        public async Task<TEntity> CreateNdo(TEntity ndo)
        {
            await ValidateNdoOnCreateOrUpdate(ndo);
            await Repo.InsertAsync(ndo);
            return ndo;
        }

        public async Task<TEntity> UpdateNdo(TEntity ndo)
        {
            await ValidateNdoOnCreateOrUpdate(ndo);
            TEntity entity = ndo.JsonClone();
            await Repo.UpdateAsync(ndo);
            return entity;
        }

        public async Task DeleteNdo(TPrimaryKey id)
        {
            await DeleteNdo(await FindNdoByIdWithInclude(id));
        }

        public async Task DeleteNdo(TEntity ndo)
        {
            await ValidateNdoOnDelete(ndo);
            ClearNavigationProp(ndo);
            await Repo.DeleteAsync(ndo);
        }

        /// <summary>
        /// 抛出 DeleteError 异常
        /// </summary>
        /// <param name="def">业务</param>
        /// <param name="defRef1">业务引用1</param>
        /// <param name="defRef2">业务引用2</param>
        /// <exception cref="UserFriendlyException"></exception>
        protected virtual void ThrowDeleteError(string def, string defRef1, string defRef2)
        {
            throw new UserFriendlyException(L("Error"), L("DeleteError", def, defRef1, defRef2, Clock.Now.ToString("yyyy-MM-dd HH:mm:ss")));
        }

        /// <summary>
        /// 抛出 RepetError 异常
        /// </summary>
        /// <param name="name">ndo的名称</param>
        /// <exception cref="UserFriendlyException">用户友好异常</exception>
        protected virtual void ThrowRepetError(string name)
        {
            throw new UserFriendlyException(L("Error"), L("RepetError", name, Clock.Now.ToString("yyyy-MM-dd HH:mm:ss")));
        }

        //
        // 摘要:
        //     抛出 ThrowUserFriendlyError 异常
        protected virtual void ThrowUserFriendlyError(string reason)
        {
            throw new UserFriendlyException(L("Error"), L("UserFriendlyError", reason, Clock.Now.ToString("yyyy-MM-dd HH:mm:ss")));
        }

        //
        // 摘要:
        //     抛出异常,传入完成带时间的提示信息
        //
        // 参数:
        //   reason:
        protected virtual void ThrowUserFriendly(string reason)
        {
            throw new UserFriendlyException(L("Error"), reason);
        }

        //
        // 摘要:
        //     清空导航属性
        //
        // 参数:
        //   obj:
        protected virtual void ClearNavigationProp(object obj)
        {
            if (obj == null)
            {
                return;
            }

            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (!propertyInfo.PropertyType.IsStructs())
                {
                    propertyInfo.SetValue(obj, null);
                }
            }
        }
    }
}
