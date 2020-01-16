using AppCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class TableTrackConfiguration : IEntityTypeConfiguration<DiningTableTrack>
    {
        public void Configure(EntityTypeBuilder<DiningTableTrack> builder)
        {
            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1);
        }
    }
}
