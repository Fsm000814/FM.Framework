using System.Threading.Tasks;
using Abp.Application.Services;
using FM.FrameWork.Sessions.Dto;

namespace FM.FrameWork.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
