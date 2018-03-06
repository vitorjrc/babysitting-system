using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace GuguDadah.Controllers
{
    public abstract class GuguDadahControllerBase: AbpController
    {
        protected GuguDadahControllerBase()
        {
            LocalizationSourceName = GuguDadahConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
