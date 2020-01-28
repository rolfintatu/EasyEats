using AppCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AppCore.Interfaces
{
    public interface IEasyEatsDbContext
    {
        DbSet<Entities.Customer> Customers { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Entities.OrderDetails> OrderDetails { get; set; }
        DbSet<Entities.Product> Products { get; set; }
        DbSet<Bill> Bills { get; set; }
        DbSet<Entities.Reservation> Reservations { get; set; }
        DbSet<Entities.Table> Tables { get; set; }


        Task<int> SaveChangesAsync(CancellationToken cancelletion);

    }
}