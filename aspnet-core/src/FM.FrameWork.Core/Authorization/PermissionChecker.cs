using Abp.Authorization;
using FM.FrameWork.Authorization.Roles;
using FM.FrameWork.Authorization.Users;

namespace FM.FrameWork.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
