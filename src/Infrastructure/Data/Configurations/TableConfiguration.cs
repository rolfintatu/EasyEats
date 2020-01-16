using AppCore.Entities;
using AppCore.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class TableConfiguration : IEntityTypeConfiguration<DiningTable>
    {
        public void Configure(EntityTypeBuilder<DiningTable> builder)
        {
            builder.HasMany<Reservation>(x => x.Reservations)
                .WithOne(x => x.Table)
                .HasForeignKey(x => x.TableId);

            builder.HasOne<DiningTableTrack>(x => x.TableTruck)
                .WithOne(x => x.DiningTable)
                .HasForeignKey<DiningTableTrack>(x => x.TableId);

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
