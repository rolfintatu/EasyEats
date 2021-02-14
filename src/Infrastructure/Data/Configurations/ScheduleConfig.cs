using Domain.Aggregates.ScheduleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class ScheduleConfig : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("Schedules");

            builder.HasMany(x => x.Reservations)
                .WithOne()
                .HasForeignKey(x => x.ScheduleId);

            builder.OwnsOne(x => x.Month, y =>
            {
                y.Property(x => x.Year).HasColumnName("SYear");
                y.Property(x => x.MonthNumber).HasColumnName("Month");
                y.Property(x => x.MountDays).HasColumnName("NumberOfDays");
            });
        }
    }
}
