

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Order.Models;

namespace DAL.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Order.Models.Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries().Where(s => s.Entity is BaseEntity && (s.State == EntityState.Added));
            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
            }
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entries = ChangeTracker.Entries().Where(s => s.Entity is BaseEntity && (s.State == EntityState.Added));
            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
            }
            
            return base.SaveChangesAsync(cancellationToken);
        } 
    }
}