using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation.AspNetCore;
using Application.Common.Interfaces;
using WebUi.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Infrastructure;

namespace WebUi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IEasyEatsDbContext>());

            Application.IoC.Config(Configuration, services);
            services.AddInfrastructure(Configuration);
            
            services.AddTransient<ICurrentUserService, CurrentUserService>();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Easy Eats v1", Version = "v1" });
                
                x.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter into field the word 'Bearer' following by space and JWT",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,

                    });

                x.AddSecurityRequirement(
                    new OpenApiSecurityRequirement{
                       {
                           new OpenApiSecurityScheme {
                                Reference = new OpenApiReference()
                                      {
                                           Id = "Bearer",
                                           Type = ReferenceType.SecurityScheme
                                       },
                                UnresolvedReference = true   },
                           new List<string>() }
                        }
                    );
                x.UseInlineDefinitionsForEnums();

            });

            services.AddSwaggerGenNewtonsoftSupport();
            services.AddDataProtection();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger(x => x.SerializeAsV2 = true);
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EasyEatsV1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
