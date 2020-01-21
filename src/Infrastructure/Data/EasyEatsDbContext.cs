using AppCore.Common;
using AppCore.Entities;
using AppCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class EasyEatsDbContext : DbContext, IEasyEatsDbContext
    {
        private readonly ICurrentUserService currentUser;

        public EasyEatsDbContext(DbContextOptions options, ICurrentUserService currentUser) 
            : base(options)
        {
            this.currentUser = currentUser;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Drink> Drinks { get; set; }

        //TODO: Add history/audit table
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var context = ChangeTracker.Context;

            foreach (var entry in context.ChangeTracker.Entries().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        //entry.Entity.CreatedBy = currentUser.UserId;
                        //entry.Entity.Created = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        //entry.Entity.LastModifiedBy = currentUser.UserId;
                        //entry.Entity.LastModified = DateTime.Now;
                        break;
                    default:
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
