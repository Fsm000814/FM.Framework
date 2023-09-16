using Abp.Data;
using Abp.Domain.Entities;
using Abp.Events.Bus.Entities;
using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using Dapper;

namespace FM.FrameWork.Database
{
    public class DapperRepositoryBase<TEntity, TPrimaryKey> : AbpDapperRepositoryBase<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly IActiveTransactionProvider _activeTransactionProvider;

        public IDapperQueryFilterExecuter DapperQueryFilterExecuter { get; set; }

        public IEntityChangeEventHelper EntityChangeEventHelper { get; set; }

        public IDapperActionFilterExecuter DapperActionFilterExecuter { get; set; }

        public virtual int? Timeout => null;

        public DapperRepositoryBase(IActiveTransactionProvider activeTransactionProvider)
        {
            _activeTransactionProvider = activeTransactionProvider;
            EntityChangeEventHelper = NullEntityChangeEventHelper.Instance;
            DapperQueryFilterExecuter = NullDapperQueryFilterExecuter.Instance;
            DapperActionFilterExecuter = NullDapperActionFilterExecuter.Instance;
        }

        public virtual DbConnection GetConnection()
        {
            return (DbConnection)_activeTransactionProvider.GetActiveConnection(ActiveTransactionProviderArgs.Empty);
        }

        public virtual async Task<DbConnection> GetConnectionAsync()
        {
            return (DbConnection)await _activeTransactionProvider.GetActiveConnectionAsync(ActiveTransactionProviderArgs.Empty).ConfigureAwait(continueOnCapturedContext: false);
        }

        public virtual async Task<DbTransaction> GetActiveTransactionAsync()
        {
            return (DbTransaction)await _activeTransactionProvider.GetActiveTransactionAsync(ActiveTransactionProviderArgs.Empty).ConfigureAwait(continueOnCapturedContext: false);
        }

        public virtual DbTransaction GetActiveTransaction()
        {
            return (DbTransaction)_activeTransactionProvider.GetActiveTransaction(ActiveTransactionProviderArgs.Empty);
        }

        public override TEntity Single(TPrimaryKey id)
        {
            return Single(CreateEqualityExpressionForId(id));
        }

        public override TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            IPredicate predicate2 = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return GetConnection().GetList<TEntity>(predicate2, null, GetActiveTransaction(), Timeout).Single();
        }

        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            return FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public override TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            IPredicate predicate2 = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return GetConnection().GetList<TEntity>(predicate2, null, GetActiveTransaction(), Timeout).FirstOrDefault();
        }

        public override TEntity Get(TPrimaryKey id)
        {
            return FirstOrDefault(id) ?? throw new EntityNotFoundException(typeof(TEntity), id);
        }

        public override IEnumerable<TEntity> GetAll()
        {
            PredicateGroup predicate = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>();
            return GetConnection().GetList<TEntity>(predicate, null, GetActiveTransaction(), Timeout);
        }

        public override IEnumerable<TEntity> Query(string query, object parameters = null)
        {
            return GetConnection().Query<TEntity>(query, parameters, GetActiveTransaction(), buffered: true, Timeout);
        }

        public override async Task<IEnumerable<TEntity>> QueryAsync(string query, object parameters = null)
        {
            return await (await GetConnectionAsync().ConfigureAwait(continueOnCapturedContext: false)).QueryAsync<TEntity>(query, parameters, await GetActiveTransactionAsync().ConfigureAwait(continueOnCapturedContext: false), Timeout).ConfigureAwait(continueOnCapturedContext: false);
        }

        public override IEnumerable<TAny> Query<TAny>(string query, object parameters = null)
        {
            return GetConnection().Query<TAny>(query, parameters, GetActiveTransaction(), buffered: true, Timeout);
        }

        public override async Task<IEnumerable<TAny>> QueryAsync<TAny>(string query, object parameters = null)
        {
            return await (await GetConnectionAsync().ConfigureAwait(continueOnCapturedContext: false)).QueryAsync<TAny>(query, parameters, await GetActiveTransactionAsync().ConfigureAwait(continueOnCapturedContext: false), Timeout).ConfigureAwait(continueOnCapturedContext: false);
        }

        public override int Execute(string query, object parameters = null, CommandType? commandType = null)
        {
            return GetConnection().Execute(query, parameters, GetActiveTransaction(), Timeout, commandType);
        }

        public override async Task<int> ExecuteAsync(string query, object parameters = null, CommandType? commandType = null)
        {
            return await (await GetConnectionAsync().ConfigureAwait(continueOnCapturedContext: false)).ExecuteAsync(query, parameters, await GetActiveTransactionAsync().ConfigureAwait(continueOnCapturedContext: false), Timeout, commandType).ConfigureAwait(continueOnCapturedContext: false);
        }

        public override IEnumerable<TEntity> GetAllPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, string sortingProperty, bool ascending = true)
        {
            IPredicate predicate2 = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return GetConnection().GetPage<TEntity>(predicate2, new List<ISort>
            {
                new Sort
                {
                    Ascending = ascending,
                    PropertyName = sortingProperty
                }
            }, pageNumber, itemsPerPage, GetActiveTransaction(), Timeout);
        }

        public override int Count(Expression<Func<TEntity, bool>> predicate)
        {
            IPredicate predicate2 = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return GetConnection().Count<TEntity>(predicate2, GetActiveTransaction(), Timeout);
        }

        public override IEnumerable<TEntity> GetSet(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, string sortingProperty, bool ascending = true)
        {
            IPredicate predicate2 = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return GetConnection().GetSet<TEntity>(predicate2, new List<ISort>
            {
                new Sort
                {
                    Ascending = ascending,
                    PropertyName = sortingProperty
                }
            }, firstResult, maxResults, GetActiveTransaction(), Timeout);
        }

        public override IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            IPredicate predicate2 = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return GetConnection().GetList<TEntity>(predicate2, null, GetActiveTransaction(), Timeout);
        }

        public override IEnumerable<TEntity> GetAllPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            IPredicate predicate2 = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return GetConnection().GetPage<TEntity>(predicate2, sortingExpression.ToSortable(ascending), pageNumber, itemsPerPage, GetActiveTransaction(), Timeout);
        }

        public override IEnumerable<TEntity> GetSet(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            IPredicate predicate2 = DapperQueryFilterExecuter.ExecuteFilter<TEntity, TPrimaryKey>(predicate);
            return GetConnection().GetSet<TEntity>(predicate2, sortingExpression.ToSortable(ascending), firstResult, maxResults, GetActiveTransaction(), Timeout);
        }

        public override void Insert(TEntity entity)
        {
            InsertAndGetId(entity);
        }

        public override void Update(TEntity entity)
        {
            EntityChangeEventHelper.TriggerEntityUpdatingEvent(entity);
            DapperActionFilterExecuter.ExecuteModificationAuditFilter<TEntity, TPrimaryKey>(entity);
            GetConnection().Update(entity, GetActiveTransaction(), Timeout);
            EntityChangeEventHelper.TriggerEntityUpdatedEventOnUowCompleted(entity);
        }

        public override void Delete(TEntity entity)
        {
            EntityChangeEventHelper.TriggerEntityDeletingEvent(entity);
            if (entity is ISoftDelete)
            {
                DapperActionFilterExecuter.ExecuteDeletionAuditFilter<TEntity, TPrimaryKey>(entity);
                GetConnection().Update(entity, GetActiveTransaction(), Timeout);
            }
            else
            {
                GetConnection().Delete(entity, GetActiveTransaction(), Timeout);
            }

            EntityChangeEventHelper.TriggerEntityDeletedEventOnUowCompleted(entity);
        }

        public override void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (TEntity item in GetAll(predicate))
            {
                Delete(item);
            }
        }

        public override TPrimaryKey InsertAndGetId(TEntity entity)
        {
            EntityChangeEventHelper.TriggerEntityCreatingEvent(entity);
            DapperActionFilterExecuter.ExecuteCreationAuditFilter<TEntity, TPrimaryKey>(entity);
            TPrimaryKey result = GetConnection().Insert(entity, GetActiveTransaction(), Timeout);
            EntityChangeEventHelper.TriggerEntityCreatedEventOnUowCompleted(entity);
            return result;
        }
    }
}
