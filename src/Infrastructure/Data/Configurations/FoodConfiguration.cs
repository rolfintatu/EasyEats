using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using AppCore.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AppCore.Common;

namespace Infrastructure.Data.Configurations
{
    public class FoodConfiguration : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> builder)
        {

            //builder.HasOne<OrderDetails>(x => x.OrderDetails)
            //    .WithMany(x => x.Foods)
            //    .HasForeignKey(x => x.OrderDetailsId);


            builder.Property(x => x.Id)
                .UseIdentityColumn(1,1);

            builder.Property(x => x.Price)
                .HasDefaultValue(0)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Quantity)
                .HasDefaultValue(1);

            builder.Property(x => x.Category)
                .HasColumnType("nvarchar(50)");

            builder.Property(x => x.Description)
                .HasDefaultValue("nvarchar(256)");

            builder.Property(x => x.Calories)
                .HasDefaultValue(0);

        }
    }
}
