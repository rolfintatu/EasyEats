using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly ConnectionString ConnectionString;

        public AppIdentityDbContext(ConnectionString connectionString)
            :base()
        { this.ConnectionString = connectionString; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.Value, 
                x => {
                        x.MigrationsAssembly("Infrastructure");
                        x.EnableRetryOnFailure(5);
                    });
        }
    }
}
