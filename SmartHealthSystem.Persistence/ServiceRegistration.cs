using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHealthSystem.Domain.Indetity;
using SmartHealthSystem.Persistence.AppDbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Veritabanı bağlantısını konfigürasyondan alıyoruz
            // services.AddDbContext<AppDbContexts>(options =>
            //options.UseSqlServer(configuration.GetConnectionString("Server=.\\SQLEXPRESS;Database=SmartHealthDb;Trusted_Connection=True;TrustServerCertificate=True;")));
            string connectionString = "Server=.\\SQLEXPRESS;Database=SmartHealthDb;Trusted_Connection=True;TrustServerCertificate=True;";

            services.AddDbContext<AppDbContexts>(options =>
                options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly(typeof(AppDbContexts).Assembly.FullName)));

            // Identity ayarlarını yapıyoruz
            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
            })
     .AddEntityFrameworkStores<AppDbContexts>()
     .AddDefaultTokenProviders();
        }
    }
}
