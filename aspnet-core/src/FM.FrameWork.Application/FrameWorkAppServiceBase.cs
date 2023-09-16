using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using FM.FrameWork.Authorization.Users;
using FM.FrameWork.MultiTenancy;
using Abp.UI;
using Abp.Timing;

namespace FM.FrameWork
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class FrameWorkAppServiceBase : ApplicationService
    {
        /// <summary>
        /// 租户管理器
        /// </summary>
        public TenantManager TenantManager { get; set; }
        /// <summary>
        /// 用户管理器
        /// </summary>

        public UserManager UserManager { get; set; }

        /// <summary>
        /// 初始化多语言
        /// </summary>

        protected FrameWorkAppServiceBase()
        {
            LocalizationSourceName = FrameWorkConsts.LocalizationSourceName;
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        /// <summary>
        /// 获取当前租户
        /// </summary>
        /// <returns></returns>
        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        /// <summary>
        /// 检查错误
        /// </summary>
        /// <param name="identityResult"></param>
        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        /// <summary>
        ///  数据为空 传入多语言
        /// </summary>
        /// <param name="str"></param>
        /// <exception cref="UserFriendlyException"></exception>
        protected void NullError(string str = "NullError")
        {
            throw new UserFriendlyException(L("Error"), L(str, Clock.Now.ToString("yyyy-MM-dd HH:mm:ss")));
        }

        /// <summary>
        /// 名称重复错误 传入多语言
        /// </summary>
        /// <param name="name">重复的名称</param>
        /// <param name="str">错误编码</param>
        /// <exception cref="UserFriendlyException"></exception>
        protected void RepetError(string name, string str = "RepetError")
        {
            throw new UserFriendlyException(L("Error"), L(str, name, Clock.Now.ToString("yyyy-MM-dd HH:mm:ss")));
        }

    }
}
