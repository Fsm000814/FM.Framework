using Abp.Data;
using Abp.Domain.Entities;
using Abp.Domain.Uow;
using Abp.Extensions;

using FM.FrameWork.Database;

using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace FM.FrameWork.Repository
{
    public class DapperEfRepositoryBase<TDbContext, TEntity, TPrimaryKey> : DapperRepositoryBase<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly IActiveTransactionProvider _activeTransactionProvider;

        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

        public ActiveTransactionProviderArgs ActiveTransactionProviderArgs => new ActiveTransactionProviderArgs
        {
            ["ContextType"] = typeof(TDbContext),
            ["MultiTenancySide"] = MultiTenancySide
        };

        public override int? Timeout
        {
            get
            {
                IUnitOfWork current = _currentUnitOfWorkProvider.Current;
                if (current == null)
                {
                    return null;
                }

                TimeSpan? timeout = current.Options.Timeout;
                if (!timeout.HasValue)
                {
                    return null;
                }

                return timeout.GetValueOrDefault().TotalSeconds.To<int>();
            }
        }

        public DapperEfRepositoryBase(IActiveTransactionProvider activeTransactionProvider, ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
            : base(activeTransactionProvider)
        {
            _activeTransactionProvider = activeTransactionProvider;
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
        }

        public override DbConnection GetConnection()
        {
            return (DbConnection)_activeTransactionProvider.GetActiveConnection(ActiveTransactionProviderArgs);
        }

        public override async Task<DbConnection> GetConnectionAsync()
        {
            return (DbConnection)await _activeTransactionProvider.GetActiveConnectionAsync(ActiveTransactionProviderArgs).ConfigureAwait(continueOnCapturedContext: false);
        }

        public override DbTransaction GetActiveTransaction()
        {
            return (DbTransaction)_activeTransactionProvider.GetActiveTransaction(ActiveTransactionProviderArgs);
        }

        public override async Task<DbTransaction> GetActiveTransactionAsync()
        {
            return (DbTransaction)await _activeTransactionProvider.GetActiveTransactionAsync(ActiveTransactionProviderArgs).ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}
