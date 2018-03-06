using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Configuration;
using GuguDadah.Configuration;
using GuguDadah.Configuration.Ui;

namespace GuguDadah.Web.Views.Shared.Components.RightSideBar
{
    public class RightSideBarViewComponent : GuguDadahViewComponent
    {
        private readonly ISettingManager _settingManager;

        public RightSideBarViewComponent(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var themeName = await _settingManager.GetSettingValueAsync(AppSettingNames.UiTheme);

            var viewModel = new RightSideBarViewModel
            {
                CurrentTheme = UiThemes.All.FirstOrDefault(t => t.CssClass == themeName)
            };

            return View(viewModel);
        }
    }
}
