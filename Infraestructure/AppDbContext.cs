using Domain.Models;
using Microsoft.EntityFrameworkCore;
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
            base.OnModelCreating(modelBuilder);
        }
    }
}
