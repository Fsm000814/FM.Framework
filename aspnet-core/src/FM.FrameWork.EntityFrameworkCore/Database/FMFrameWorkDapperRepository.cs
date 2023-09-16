using Abp.Data;
using Abp.Domain.Uow;
using FM.FrameWork.EntityFrameworkCore;
using FM.FrameWork.Authorization.Roles;

namespace FM.FrameWork.Database
{
    public class FMFrameWorkDapperRepository : DapperEfRepositoryBase<FrameWorkDbContext, Role>
    {
        protected readonly IActiveTransactionProvider ActiveTransactionProvider;

        public FMFrameWorkDapperRepository(IActiveTransactionProvider activeTransactionProvider,
            ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(activeTransactionProvider,
            currentUnitOfWorkProvider)
        {
            ActiveTransactionProvider = activeTransactionProvider;
        }
    }
}
