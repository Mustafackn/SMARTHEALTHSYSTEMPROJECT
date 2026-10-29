using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHealthSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Persistence.Configuration
{
    public class TestResultConfiguration : IEntityTypeConfiguration<TestResult>
    {
        public void Configure(EntityTypeBuilder<TestResult> builder)
        {
            // DiabetesTestData ile 1:1 ilişki
            builder.HasOne(tr => tr.DiabetesTestData)
                   .WithOne(dt => dt.TestResult)
                   .HasForeignKey<DiabetesTestData>(dt => dt.TestResultId)
                   .OnDelete(DeleteBehavior.Cascade);
            // AppUser (Guid) ile ilişki
            builder.HasOne(tr => tr.User)
                   .WithMany()
                   .HasForeignKey(tr => tr.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // HeartTestData ile 1:1 ilişki
            builder.HasOne(tr => tr.HeartTestData)
                   .WithOne(ht => ht.TestResult)
                   .HasForeignKey<HeartTestData>(ht => ht.TestResultId);

            builder.HasOne(tr => tr.KidneyTestData)
                   .WithOne(ht => ht.TestResult)
                   .HasForeignKey<KidneyTestData>(ht => ht.TestResultId);

            builder.HasOne(tr => tr.CancerTestData)
                   .WithOne(ct => ct.TestResult)
                   .HasForeignKey<CancerTestData>(ct => ct.TestResultId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(tr => tr.CardiovascularTestData)
       .WithOne(cv => cv.TestResult)
       .HasForeignKey<CardiovascularTestData>(cv => cv.TestResultId)
       .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
