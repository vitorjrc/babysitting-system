using System.Threading.Tasks;
using GuguDadah.Configuration.Dto;

namespace GuguDadah.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
