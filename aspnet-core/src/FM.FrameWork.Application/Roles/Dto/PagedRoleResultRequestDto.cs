using Abp.Application.Services.Dto;

namespace FM.FrameWork.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

