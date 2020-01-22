using AppCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AppCore.Interfaces
{
    public interface IEasyEatsDbContext
    {
        DbSet<Entities.Drink> Drinks { get; set; }
        DbSet<Entities.Food> Foods { get; set; }
        DbSet<OrderDetails> OrderDetails { get; set; }
        DbSet<Order> Orders { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancelletion);

    }
}