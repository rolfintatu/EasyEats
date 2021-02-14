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
using Infrastructure.Data.Repositories;

namespace Infrastructure
{
    public static class IoC
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(
                    new ConnectionString(configuration.GetConnectionString("IdentityConnection"))
                );

            services.AddSingleton(
                    new JwtCfg(configuration.GetSection("SecretKey").Value)
                );

            services.AddDbContext<AppIdentityDbContext>();

            services.AddDbContext<EasyEatsDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("EasyEatsConnection"), x => x.MigrationsAssembly("Infrastructure")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddSignInManager()
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

            services.AddTransient<AppIdentityDbContext>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddScoped<IDateTime, DateTimeService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IStorageService, AzureStorage>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IScheduleRepository, ScheduleRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddDataProtection();

            return services;
        }
    }
}
