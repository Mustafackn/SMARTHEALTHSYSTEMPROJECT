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
        }
    }
}
