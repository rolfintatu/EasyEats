using AppCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1);

            builder.HasOne(x => x.Bill)
                .WithOne(x => x.Order)
                .HasForeignKey<Bill>(x => x.OrderId);

            builder.HasOne(x => x.OrderDetails)
                .WithOne(x => x.Order)
                .HasForeignKey<OrderDetails>(x => x.OrderId);


        }
    }
}
