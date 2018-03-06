using System.Threading.Tasks;
using Abp.Application.Services;
using GuguDadah.Authorization.Accounts.Dto;

namespace GuguDadah.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
