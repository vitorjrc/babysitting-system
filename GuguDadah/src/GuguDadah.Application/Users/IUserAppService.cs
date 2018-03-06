using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GuguDadah.Roles.Dto;
using GuguDadah.Users.Dto;

namespace GuguDadah.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
