using System.Threading.Tasks;
using Abp.Application.Services;
using FM.FrameWork.Authorization.Accounts.Dto;

namespace FM.FrameWork.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
