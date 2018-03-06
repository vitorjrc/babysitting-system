using System.Collections.Generic;
using GuguDadah.Roles.Dto;
using GuguDadah.Users.Dto;

namespace GuguDadah.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
