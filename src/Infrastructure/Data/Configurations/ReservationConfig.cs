using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class ReservationConfig : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(x => x.Id)
                   .UseIdentityColumn(1, 1);

            builder.OwnsOne(x => x.Date);

            builder.Property(x => x.Status)
                .HasColumnType("nvarchar(30)")
                .HasDefaultValue(ReservationStatus.Waiting);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.CustomerId);

            builder.HasOne(x => x.Table)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.TableId);

            builder.HasOne(x => x.Order)
                .WithOne(x => x.Reservation)
                .HasForeignKey<Reservation>(x => x.OrderId);
        }
    }
}
