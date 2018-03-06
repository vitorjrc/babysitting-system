using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace GuguDadah.EntityFrameworkCore
{
    public static class GuguDadahDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<GuguDadahDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<GuguDadahDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
