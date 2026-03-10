using SmartHealthSystem.Domain.Common;
using SmartHealthSystem.Domain.Entites.SmartHealthSystem.Domain.Entities;
using SmartHealthSystem.Domain.Indetity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Domain.Entites
{
    public class TestResult : BaseEntity
    {
        // AppUser IdentityUser<Guid> olduğu için UserId burada Guid olmalı
        public Guid UserId { get; set; }
        public virtual AppUser User { get; set; } = null!;

        public int TestTypeId { get; set; }
        public virtual TestType TestType { get; set; } = null!;

        public double RiskPercentage { get; set; }
        public double HealthScore { get; set; }
        public string Recommendations { get; set; } = string.Empty;

        // Bire-bir ilişki: Bu sonucun kalp testi detayları
        public virtual HeartTestData? HeartTestData { get; set; }
        public virtual KidneyTestData? KidneyTestData { get; set; }
        public virtual CancerTestData? CancerTestData { get; set; }
    }
}
