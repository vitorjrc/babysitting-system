using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using GuguDadah.Authorization.Roles;
using GuguDadah.Authorization.Users;
using GuguDadah.MultiTenancy;

namespace GuguDadah.EntityFrameworkCore
{
    public class GuguDadahDbContext : AbpZeroDbContext<Tenant, Role, User, GuguDadahDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public GuguDadahDbContext(DbContextOptions<GuguDadahDbContext> options)
            : base(options)
        {
        }
    }
}
