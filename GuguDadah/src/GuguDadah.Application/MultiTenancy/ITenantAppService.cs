using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GuguDadah.MultiTenancy.Dto;

namespace GuguDadah.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
