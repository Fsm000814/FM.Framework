using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace FM.FrameWork.Controllers
{
    public abstract class FrameWorkControllerBase: AbpController
    {
        protected FrameWorkControllerBase()
        {
            LocalizationSourceName = FrameWorkConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
