using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using GuguDadah.Configuration.Dto;

namespace GuguDadah.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : GuguDadahAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
