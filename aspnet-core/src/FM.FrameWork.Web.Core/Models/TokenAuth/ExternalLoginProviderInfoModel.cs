using Abp.AutoMapper;
using FM.FrameWork.Authentication.External;

namespace FM.FrameWork.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
