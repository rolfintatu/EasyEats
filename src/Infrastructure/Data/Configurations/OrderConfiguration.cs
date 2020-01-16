using AppCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.HasOne<OrderDetails>(x => x.OrderDetails)
                .WithOne(x => x.Order)
                .HasForeignKey<OrderDetails>(x => x.OrderId);

            builder.HasOne<Bill>(x => x.Bill)
                .WithOne(x => x.Order)
                .HasForeignKey<Bill>(x => x.OrderId);

            builder.HasOne<DiningTableTrack>(x => x.TableTrack)
                .WithOne(x => x.Order)
                .HasForeignKey<DiningTableTrack>(x => x.OrderId);

            builder.ToTable("Orders");

            builder.Property(x => x.Id)
                .UseIdentityColumn(1,1);
        }
    }
}
