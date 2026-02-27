using Microsoft.Extensions.DependencyInjection;
using SmartHealthSystem.Application.Abstractions.Tokenss;
using SmartHealthSystem.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            // Sisteme diyoruz ki: ITokenHandler istendiğinde TokenHandler sınıfını ver.
            services.AddScoped<ITokenHandler, TokenHandler>();
        }
    }
}
