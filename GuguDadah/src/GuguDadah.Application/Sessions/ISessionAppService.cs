using System.Threading.Tasks;
using Abp.Application.Services;
using GuguDadah.Sessions.Dto;

namespace GuguDadah.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
