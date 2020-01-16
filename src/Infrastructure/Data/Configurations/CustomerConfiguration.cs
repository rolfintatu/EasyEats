using AppCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(x => x.Id)
                .HasColumnType("nvarchar(256)");

            builder.Property(x => x.Name)
                .HasColumnType("nvarchar(50)");

            builder.Property(x => x.Address)
                .HasColumnType("nvarchar(128)");

            builder.Property(x => x.Phone)
                .HasMaxLength(10);
        }
    }
}
