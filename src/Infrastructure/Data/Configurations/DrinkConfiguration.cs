using AppCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class DrinkConfiguration : IEntityTypeConfiguration<Drink>
    {
        public void Configure(EntityTypeBuilder<Drink> builder)
        {
            builder.HasOne<OrderDetails>(x => x.OrderDetails)
                .WithMany(x => x.Drinks)
                .HasForeignKey(x => x.OrderDetailsId);

            builder.Property(x => x.Name)
                .HasColumnType("nvarchar(100)");

            builder.Property(x => x.Description)
                .HasColumnType("nvarchar(256)");

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(x => x.Category)
                .HasColumnType("nvarchar(50)");

            builder.Property(x => x.Quantity)
                .HasDefaultValue(1);

            builder.Property(x => x.Calories)
                .HasDefaultValue(0);

        }
    }
}
