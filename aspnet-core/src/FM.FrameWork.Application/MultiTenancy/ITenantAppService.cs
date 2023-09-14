using Abp.Application.Services;
using FM.FrameWork.MultiTenancy.Dto;

namespace FM.FrameWork.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

