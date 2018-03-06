using Abp.Authorization;
using GuguDadah.Authorization.Roles;
using GuguDadah.Authorization.Users;

namespace GuguDadah.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
