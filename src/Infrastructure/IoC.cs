using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Infrastructure.Data;
using Application.Common.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Services;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{
    public static class IoC
    {
        public static void Config(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("IdentityConnection"), x => x.MigrationsAssembly("Infrastructure")));

            services.AddDbContext<EasyEatsDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("EasyEatsConnection"), x => x.MigrationsAssembly("Infrastructure")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password = new PasswordOptions
                {
                    RequiredLength = 6
                };

            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(x =>
              {
                  x.RequireHttpsMetadata = false;
                  x.SaveToken = true;

                  x.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("SecretKey"))),
                      ValidateIssuer = false,
                      ValidateAudience = false,
                      ValidateLifetime = true,
                      ClockSkew = TimeSpan.FromMinutes(5)
                  };
              });

            services.AddTransient<IEasyEatsDbContext, EasyEatsDbContext>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddScoped<IEasyEatsDbContext>(x => x.GetService<EasyEatsDbContext>());
            services.AddScoped<IDateTime, DateTimeService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IStorageService, AzureStorage>();
            services.AddDataProtection();
        }
    }
}
