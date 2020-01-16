using AppCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasOne<Customer>(x => x.Customer)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.CustomerId);

            builder.HasOne<DiningTable>(x => x.Table)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.TableId);

            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1);

        }
    }
}
