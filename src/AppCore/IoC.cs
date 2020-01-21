using AppCore.Behaviours;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutoMapper;
using AppCore.Mapping;

namespace AppCore
{
    public static class IoC
    {
        public static void Config(
            IConfiguration configuration,
            IServiceCollection services
            )
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));
            services.AddSingleton(
                new MapperConfiguration(x => x.AddProfile(new MappingProfile())).CreateMapper()
                );
        }
    }
}
