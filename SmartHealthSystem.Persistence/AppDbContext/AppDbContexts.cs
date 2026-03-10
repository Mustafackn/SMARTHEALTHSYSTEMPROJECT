using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHealthSystem.Domain.Entites;
using SmartHealthSystem.Domain.Entites.SmartHealthSystem.Domain.Entities;
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

        // Yeni Tablolarımız
        public DbSet<TestType> TestTypes { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<HeartTestData> HeartTestDatas { get; set; }
        public DbSet<KidneyTestData> KidneyTestDatas { get; set; }
        public DbSet<CancerTestData> CancerTestDatas { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Bu satır sayesinde aşağıdaki Configuration sınıfları otomatik okunacak
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
