using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimunYashirInfrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirPersistence
{
    public class MainDbContext : DbContext
    {
        public MainDbContext()
        {
        }

        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options)
        {
        }

        private IConfiguration conf { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<Package> Packages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=mimunyashir.database.windows.net;Initial Catalog=mimunYashir;User ID=mimunyashir;Password=ZXCVasdf1;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.FirstName).IsRequired(true);
                entity.Property(e => e.LastName).IsRequired(true);
                entity.Property(e => e.Id).IsRequired(true);

                entity.HasMany(e => e.Contracts)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Contract>(entity =>
            {
                entity.Property(e => e.Name).IsRequired(true);
                entity.Property(e => e.Id).IsRequired(true);
                entity.Property(e => e.Type).IsRequired(true);

                entity.HasMany(e => e.Packages)
                .WithOne(e => e.Contract)
                .HasForeignKey(e => e.ContractId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Package>(entity =>
            {
                entity.Property(e => e.PackageType).IsRequired(true);
                entity.Property(e => e.Name).IsRequired(true);
                entity.Property(e => e.Quantity).IsRequired(true);
            });
        }
    }
}
