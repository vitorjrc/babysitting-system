using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using GuguDadah.Configuration;
using GuguDadah.Web;

namespace GuguDadah.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class GuguDadahDbContextFactory : IDesignTimeDbContextFactory<GuguDadahDbContext>
    {
        public GuguDadahDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<GuguDadahDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            GuguDadahDbContextConfigurer.Configure(builder, configuration.GetConnectionString(GuguDadahConsts.ConnectionStringName));

            return new GuguDadahDbContext(builder.Options);
        }
    }
}
