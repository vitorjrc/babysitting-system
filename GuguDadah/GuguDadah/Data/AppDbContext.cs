using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GuguDadah.Data {

    public class AppDbContext : DbContext {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Professional> Professionals { get; set; }
        public DbSet<Work> Works { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            // define nome do esquema
            modelBuilder.HasDefaultSchema("GuguDadah");

            // transforma classes em tabelas SQL
            modelBuilder.Entity<Professional>().ToTable("Professional");
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Work>().ToTable("Work");

        }
    }
}
