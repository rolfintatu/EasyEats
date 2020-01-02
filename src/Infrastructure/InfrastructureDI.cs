using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Infrastructure.Data;

namespace Infrastructure.Identity
{
    public static class InfrastructureDI
    {
        public static void Config(IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("IdentityConnection"), x =>x.MigrationsAssembly("Infrastructure")));

            services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("EasyEatsConnection"), x => x.MigrationsAssembly("Infrastructure")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                
            });

        }
    }
}
