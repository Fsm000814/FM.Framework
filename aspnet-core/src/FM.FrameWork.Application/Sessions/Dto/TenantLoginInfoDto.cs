using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using FM.FrameWork.MultiTenancy;

namespace FM.FrameWork.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
