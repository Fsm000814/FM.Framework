using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.Extensions;

using FM.FrameWork.Database;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.EntityFrameworkCore.Extenstions
{
    public class DatabaseChecker<TDbContext> : IDatabaseChecker<TDbContext>, IDatabaseChecker where TDbContext : DbContext
    {
        private readonly IDbContextProvider<TDbContext> _dbContextProvider;

        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public DatabaseChecker(IDbContextProvider<TDbContext> dbContextProvider, IUnitOfWorkManager unitOfWorkManager)
        {
            _dbContextProvider = dbContextProvider;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public bool Exist(string connectionString)
        {
            if (connectionString.IsNullOrEmpty())
            {
                return true;
            }

            try
            {
                using IUnitOfWorkCompleteHandle unitOfWorkCompleteHandle = _unitOfWorkManager.Begin();
                using (_unitOfWorkManager.Current.SetTenantId(null))
                {
                    _dbContextProvider.GetDbContext().Database.OpenConnection();
                    unitOfWorkCompleteHandle.Complete();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public virtual DbContext GetDbContext()
        {
            return _dbContextProvider.GetDbContext();
        }
    }
}
