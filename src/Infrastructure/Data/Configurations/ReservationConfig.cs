using Domain.Aggregates.ScheduleAggregate;
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
            builder.Property(x => x.Id);

            builder.Property(x => x.Stage)
                .HasColumnType("nvarchar(30)")
                .HasDefaultValue(ReservationStatus.New);
        }
    }
}
