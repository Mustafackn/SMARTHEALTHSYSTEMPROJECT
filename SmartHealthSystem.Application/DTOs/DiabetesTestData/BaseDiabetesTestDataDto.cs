using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.DiabetesTestData
{
    public abstract class BaseDiabetesTestDataDto
    {
        public string Gender { get; set; } = null!;
        public float Age { get; set; }
        public int Hypertension { get; set; }
        public int HeartDisease { get; set; }
        public string SmokingHistory { get; set; } = null!;
        public float BMI { get; set; }
        public float HbA1cLevel { get; set; }
        public float BloodGlucoseLevel { get; set; }
    }
}
