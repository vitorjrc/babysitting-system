using Abp.AspNetCore.Mvc.ViewComponents;

namespace GuguDadah.Web.Views
{
    public abstract class GuguDadahViewComponent : AbpViewComponent
    {
        protected GuguDadahViewComponent()
        {
            LocalizationSourceName = GuguDadahConsts.LocalizationSourceName;
        }
    }
}
