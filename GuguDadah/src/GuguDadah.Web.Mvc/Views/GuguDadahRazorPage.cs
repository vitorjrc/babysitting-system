using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;

namespace GuguDadah.Web.Views
{
    public abstract class GuguDadahRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected GuguDadahRazorPage()
        {
            LocalizationSourceName = GuguDadahConsts.LocalizationSourceName;
        }
    }
}
