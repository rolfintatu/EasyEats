using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class TableConfig : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(x => x.Status)
                .HasColumnType("varchar(30)")
                .HasDefaultValue(TableStatus.Open);

            builder.Property(x => x.ChairsCount)
                .HasMaxLength(4)
                .HasDefaultValue(1);
        }
    }
}
