using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CardiovascularTestData.Queries.GetCardioResultsByUserId
{
    public class GetCardioResultsByUserIdQueryResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int Age { get; set; }
        public int Gender { get; set; }
        public int Height { get; set; }
        public float Weight { get; set; }
        public int SystolicBP { get; set; }
        public int DiastolicBP { get; set; }
        public int Cholesterol { get; set; }
        public int Glucose { get; set; }
        public bool IsSmoking { get; set; }
        public bool IsAlcoholConsumer { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

        // TestResult tablosundan gelen AI verileri
        public double RiskPercentage { get; set; }
        public int HealthScore { get; set; }
        public string Recommendations { get; set; } = string.Empty;
    }
}
