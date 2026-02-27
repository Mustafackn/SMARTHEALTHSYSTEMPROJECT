using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHealthSystem.Domain.Indetity;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SmartHealthSystem.Persistence.AppDbContext
{
    public class AppDbContexts : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContexts(DbContextOptions<AppDbContexts> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Mevcut assembly (Persistence) içindeki tüm IEntityTypeConfiguration arayüzlerini otomatik uygular.
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
