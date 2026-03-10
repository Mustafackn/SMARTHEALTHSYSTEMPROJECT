using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHealthSystem.Domain.Indetity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Persistence.IndetityConfiguration
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.ToTable("Roles");

            // Seed Data: Roller veritabanına bu ID'lerle sabitlenecek
            builder.HasData(
              new AppRole
              {
                  Id = Guid.Parse("A1234567-B123-C123-D123-E12345678910"), // Sabit GUID
                  Name = "Admin",
                  NormalizedName = "ADMIN",
                  ConcurrencyStamp = "1"
              },
        new AppRole
        {
            Id = Guid.Parse("B1234567-C123-D123-E123-F12345678911"), // Sabit GUID
            Name = "Doctor",
            NormalizedName = "DOCTOR",
            ConcurrencyStamp = "2"
        },
        new AppRole
        {
            Id = Guid.Parse("C1234567-D123-E123-F123-A12345678912"), // Sabit GUID
            Name = "User",
            NormalizedName = "USER",
            ConcurrencyStamp = "3"
        }
            );
        }
    }
}
