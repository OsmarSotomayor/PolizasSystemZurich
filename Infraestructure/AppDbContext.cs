using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Policy> Policies { get; set; }
        public DbSet<Client> Clients { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Policy>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Type).IsRequired().HasMaxLength(50);
                entity.Property(p => p.StartDate).IsRequired();
                entity.Property(p => p.ExpirationDate).IsRequired();
                entity.Property(p => p.Amount).HasColumnType("decimal(18,2)");
                entity.Property(p => p.State).HasMaxLength(20);
                entity.Property(p => p.ClientId).IsRequired();
            });

            modelBuilder.Entity<Client>()
           .HasKey(c => c.IdentificationNumber);

            modelBuilder.Entity<Client>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Client>()
                .Property(c => c.FullName)
                .IsRequired();

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Policies)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }

    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=tcp:zurichserve.database.windows.net,1433;Initial Catalog=Authentication;Persist Security Info=False;User ID=inicio_sql;Password=Soto17b*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
