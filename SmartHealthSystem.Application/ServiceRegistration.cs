using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SmartHealthSystem.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // Yeni kayıt yöntemi bu şekildedir:
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(assembly);
            });

            services.AddValidatorsFromAssembly(assembly);
            services.AddAutoMapper(assembly);
        }
    }
}
