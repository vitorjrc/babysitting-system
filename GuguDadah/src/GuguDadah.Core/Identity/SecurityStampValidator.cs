using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using GuguDadah.Authorization.Roles;
using GuguDadah.Authorization.Users;
using GuguDadah.MultiTenancy;

namespace GuguDadah.Identity
{
    public class SecurityStampValidator : AbpSecurityStampValidator<Tenant, Role, User>
    {
        public SecurityStampValidator(
            IOptions<SecurityStampValidatorOptions> options, 
            SignInManager signInManager,
            ISystemClock systemClock) 
            : base(
                  options, 
                  signInManager, 
                  systemClock)
        {
        }
    }
}
