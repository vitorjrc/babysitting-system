using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.MultiTenancy;
using Abp.Zero.EntityFrameworkCore;

namespace GuguDadah.EntityFrameworkCore
{
    public class AbpZeroDbMigrator : AbpZeroDbMigrator<GuguDadahDbContext>
    {
        public AbpZeroDbMigrator(
            IUnitOfWorkManager unitOfWorkManager,
            IDbPerTenantConnectionStringResolver connectionStringResolver,
            IDbContextResolver dbContextResolver)
            : base(
                unitOfWorkManager,
                connectionStringResolver,
                dbContextResolver)
        {
        }
    }
}
