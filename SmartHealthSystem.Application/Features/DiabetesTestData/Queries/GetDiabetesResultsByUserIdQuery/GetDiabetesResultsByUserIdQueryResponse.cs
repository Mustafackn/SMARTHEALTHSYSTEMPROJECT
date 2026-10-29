using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.DiabetesTestData.Queries.GetDiabetesResultsByUserIdQuery
{
    public class GetDiabetesResultsByUserIdQueryResponse
    {
        public int Id { get; set; }
        public string Gender { get; set; } = null!;
        public float Age { get; set; }
        public int Hypertension { get; set; }
        public int HeartDisease { get; set; }
        public string SmokingHistory { get; set; } = null!;
        public float BMI { get; set; }
        public float HbA1cLevel { get; set; }
        public float BloodGlucoseLevel { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
