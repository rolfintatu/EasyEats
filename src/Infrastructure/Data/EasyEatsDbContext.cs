using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
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
        private readonly IDateTime dateTime;

        public EasyEatsDbContext(DbContextOptions options
            , ICurrentUserService currentUser
            , IDateTime dateTime) 
            : base(options)
        {
            this.currentUser = currentUser;
            this.dateTime = dateTime;
        }

        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        //TODO: Add history/audit table
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var context = ChangeTracker.Context;

            foreach (var entry in context.ChangeTracker.Entries<AuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = currentUser.UserId ?? "NewUser";
                        entry.Entity.Created = dateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = currentUser.UserId ?? "NewUser";
                        entry.Entity.LastModified = dateTime.UtcNow;
                        break;
                    default:
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
